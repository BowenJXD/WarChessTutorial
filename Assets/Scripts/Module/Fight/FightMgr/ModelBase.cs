using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// Battle unit base class
    /// </summary>
    public class ModelBase : MonoBehaviour
    {
        /// <summary>
        /// Object id
        /// </summary>
        public int id;
        
        /// <summary>
        /// Config data
        /// </summary>
        public Dictionary<string, string> data;

        public int step;

        public int attack;

        public int type;
        
        public int maxHp;

        public int curHp;

        public int rowIndex;

        public int colIndex;

        [HideInInspector] public SpriteRenderer bodySp;
        
        /// <summary>
        /// target object when stopped moving
        /// </summary>
        [HideInInspector] public GameObject stopObj;

        [HideInInspector] public Animator ani;

        void Awake()
        {
            bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
            stopObj = transform.Find("stop").gameObject;
            ani = transform.Find("body").GetComponent<Animator>();
        }

        protected virtual void OnEnable()
        {
            AddEvents();
        }

        protected virtual void AddEvents()
        {
            GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.AddEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        protected virtual void OnDisable()
        {
            RemoveEvents();
        }

        protected virtual void RemoveEvents()
        {
            GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.RemoveEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        protected virtual void OnSelectCallback(Object obj)
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
            
            GameApp.MapManager.ShowStepGrid(this, step);
        }
        
        protected virtual void OnUnselectCallback(Object obj)
        {
            bodySp.color = Color.white;
            
            GameApp.MapManager.HideStepGrid(this, step);
        }
    }
}