using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    public class BaseView : MonoBehaviour, IBaseView
    {
        public int ViewId { get; set; }
        public BaseController Controller { get; set; }

        protected bool isInit;

        protected Canvas canvas;

        protected Dictionary<string, GameObject> cacheGOs = new Dictionary<string, GameObject>();

        void Awake()
        {
            canvas = GetComponent<Canvas>();
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        protected virtual void OnAwake()
        {
            
        }

        protected virtual void OnStart()
        {
            
        }

        # region IBaseViewFunc
        
        public bool IsInit()
        {
            return isInit;
        }

        public bool IsShow()
        {
            return canvas.enabled;
        }

        public virtual void InitUI()
        {
            
        }

        public virtual void InitData()
        {
            isInit = true;
        }

        public virtual void Open(params object[] args)
        {
            
        }

        public virtual void Close(params object[] args)
        {
            SetVisible(false);
        }

        public void DestroyView()
        {
            Controller = null;
            Destroy(gameObject);
        }

        public void ApplyFunc(string eventName, params object[] args)
        {
            Controller.ApplyFunc(eventName, args);
        }

        public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
        {
            Controller.ApplyControllerFunc(controllerKey, eventName, args);
        }

        public void SetVisible(bool visible)
        {
            canvas.enabled = visible;
        }
        
        # endregion

        public GameObject Find(string res)
        {
            if (cacheGOs.ContainsKey(res))
            {
                return cacheGOs[res];
            }
            cacheGOs.Add(res, transform.Find(res).gameObject);
            return cacheGOs[res];
        }

        public T Find<T>(string res) where T : Component
        {
            GameObject go = Find(res);
            return go.GetComponent<T>();
        }
    }
}