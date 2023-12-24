using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace WarChess
{
    public class ViewInfo
    {
        public string prefabName;
        public Transform parentTf;
        public BaseController controller;
        public int sortingOrder;
    }
    
    public class ViewManager
    {
        public Transform canvasTf;

        public Transform worldCanvasTf;

        protected Dictionary<int, IBaseView> opens;
        
        protected Dictionary<int, IBaseView> viewCaches;
        
        protected Dictionary<int, ViewInfo> viewInfos;
        
        public ViewManager()
        {
            canvasTf = GameObject.Find("Canvas").transform;
            worldCanvasTf = GameObject.Find("WorldCanvas").transform;
            opens = new Dictionary<int, IBaseView>();
            viewCaches = new Dictionary<int, IBaseView>();
            viewInfos = new Dictionary<int, ViewInfo>();
        }
        
        public void Register(EViewType viewType, ViewInfo viewInfo)
        {
            Register((int)viewType, viewInfo);
        }
        
        /// <summary>
        /// Register view info
        /// </summary>
        /// <param name="key"></param>
        /// <param name="viewInfo"></param>
        public void Register(int key, ViewInfo viewInfo)
        {
            if (!viewInfos.ContainsKey(key))
            {
                viewInfos.Add(key, viewInfo);
            }
        }

        /// <summary>
        /// Unregister view info
        /// </summary>
        /// <param name="key"></param>
        public void Unregister(int key)
        {
            if (viewInfos.ContainsKey(key))
            {
                viewInfos.Remove(key);
            }
        }

        public void RemoveView(int key)
        {
            viewInfos.Remove(key);
            viewCaches.Remove(key);
            opens.Remove(key);
        }

        public void RemoveViewByController(BaseController ctl)
        {
            foreach (var item in viewInfos)
            {
                if (item.Value.controller == ctl)
                {
                    RemoveView(item.Key);
                    break;
                }
            }
        }

        public bool IsOpen(int key)
        {
            return opens.ContainsKey(key);
        }

        public IBaseView GetView(int key)
        {
            if (opens.ContainsKey(key))
            {
                return opens[key];
            }
            if (viewCaches.ContainsKey(key))
            {
                return viewCaches[key];
            }

            return null;
        }
        
        public T GetView<T>(int key) where T : class, IBaseView
        {
            IBaseView result = GetView(key);
            if (result != null)
            {
                return result as T;
            }

            return null;
        }

        public void Destroy(int key)
        {
            IBaseView oldView = GetView(key);
            if (oldView != null)
            {
                Unregister(key);
                oldView.DestroyView();
                viewCaches.Remove(key);
            }
        }

        public void Close(EViewType viewType, params object[] args)
        {
            Close((int)viewType, args);
        }
        
        public void Close(int key, params object[] args)
        {
            if (!IsOpen(key))
            {
                return;
            }

            IBaseView view = GetView(key);
            if (view != null)
            {
                view.Close(args);
                viewInfos[key].controller.CloseView(view);
                opens.Remove(key); //
            }
        }
        
        public void Open(EViewType viewType, params object[] args)
        {
            Open((int)viewType, args);
        }
        
        public void Open(int key, params object[] args)
        {
            IBaseView view = GetView(key);
            ViewInfo viewInfo = viewInfos[key];
            if (view == null)
            {
                // load resource if view does not exist
                string type = ((EViewType)key).ToString(); // type's string corresponds to the scripts' name
                GameObject uiGO = Object.Instantiate(Resources.Load($"View/{viewInfo.prefabName}"), viewInfo.parentTf) as GameObject;
                Canvas canvas = uiGO.GetComponent<Canvas>();
                if (!canvas)
                {
                    canvas = uiGO.AddComponent<Canvas>();
                }

                if (!uiGO.GetComponent<GraphicRaycaster>())
                {
                    uiGO.AddComponent<GraphicRaycaster>();
                }
                
                canvas.overrideSorting = true;
                canvas.sortingOrder = viewInfo.sortingOrder;
                
                view = uiGO.AddComponent(Type.GetType($"WarChess.{type}")) as IBaseView; // add corresponding view script
                if (view != null)
                {
                    view.ViewId = key;
                    view.Controller = viewInfo.controller;
                    viewCaches.Add(key, view);
                    viewInfo.controller.OnLoadView(view);
                }
                else
                {
                    throw new Exception($"ERROR: {type} is not a view script");
                }
            }

            if (IsOpen(key))
            {
                return;
            }
            opens.Add(key, view);

            if (view.IsInit())
            {
                view.SetVisible(true);
            }
            else
            {
                view.InitUI();
                view.InitData();
            }
            view.Open(args);
            viewInfo.controller.OpenView(view);
        }
    }
}