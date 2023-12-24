using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    public class BaseController
    {
        protected Dictionary<string, Action<Object[]>> messages;
        
        protected BaseModel model;
        
        public BaseController()
        {
            messages = new Dictionary<string, Action<Object[]>>();
        }

        /// <summary>
        /// Initializing function that is called when the controller is registered (must be executed when a controller is initialized)
        /// </summary>
        public virtual void Init()
        {
            
        }
        
        # region View
        
        public virtual void OnLoadView(IBaseView view)
        {
            
        }
        
        public virtual void OpenView(IBaseView view)
        {
            
        }
        
        public virtual void CloseView(IBaseView view)
        {
            
        }
        
        # endregion
        
        # region Func
        
        public void RegisterFunc(string eventName, Action<Object[]> callback)
        {
            if (messages.ContainsKey(eventName))
            {
                messages[eventName] += callback;
            }
            else
            {
                messages.Add(eventName, callback);
            }
        }
        
        public void UnRegisterFunc(string eventName)
        {
            if (messages.ContainsKey(eventName))
            {
                messages.Remove(eventName);
            }
        }
        
        public void ApplyFunc(string eventName, params object[] args)
        {
            if (messages.ContainsKey(eventName))
            {
                messages[eventName].Invoke(args);
            }
            else
            {
                Debug.LogError($"ERROR: {eventName} not found!");
            }
        }
        
        public void ApplyControllerFunc(EControllerType type, string eventName, params object[] args)
        {
            ApplyControllerFunc((int)type, eventName, args);
        }
        
        public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
        {
            GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);
        }

        # endregion
        
        # region Model
        
        public void SetModel(BaseModel mdl)
        {
            this.model = mdl;
            this.model.controller = this;
        }
        
        public BaseModel GetModel()
        {
            return model;
        }
        
        public T GetModel<T>() where T : BaseModel
        {
            return model as T;
        }
        
        public BaseModel GetControllerModel(int controllerKey)
        {
            BaseModel mdl = GameApp.ControllerManager.GetControllerModel(controllerKey);
            return mdl;
        }
        
        # endregion
        
        public virtual void Destroy()
        {
            RemoveModuleEvent();
            RemoveGlobalEvent();
        }
        
        # region Event
        
        public virtual void InitModuleEvent()
        {
            
        }
        
        public virtual void RemoveModuleEvent()
        {
            
        }

        public virtual void InitGlobalEvent()
        {
            
        }
        
        public virtual void RemoveGlobalEvent()
        {
            
        }
        
        # endregion
    }
}