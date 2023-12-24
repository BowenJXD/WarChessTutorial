using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// 
    /// </summary>
    public enum EBlockType
    {
        Null,
        Obstacle,
    }

    public enum EBlockState
    {
        /// <summary>
        /// 
        /// </summary>
        Select,
        Grid,
        Dir,
    }
    
    /// <summary>
    /// A block on the map's grid
    /// </summary>
    public class Block : MonoBehaviour
    {
        public int rowIndex;
        public int colIndex;
        public EBlockType type;
        
        protected Dictionary<EBlockState, SpriteRenderer> sps = new Dictionary<EBlockState, SpriteRenderer>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var sp = child.GetComponent<SpriteRenderer>();
                if (sp != null)
                {
                    EBlockState state = (EBlockState) Enum.Parse(typeof(EBlockState), child.name, true);
                    sps.Add(state, sp);
                }
            }
        }

        private void OnEnable()
        {
            GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
        }

        private void OnDisable()
        {
            GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
        }

        void OnSelectCallback(Object obj)
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
        }

        public void ShowGrid(Color color)
        {
            sps[EBlockState.Grid].enabled = true;
            sps[EBlockState.Grid].color = color;
        }
        
        public void HideGrid()
        {
            sps[EBlockState.Grid].enabled = false;
        }
        
        void OnMouseEnter()
        {
            sps[EBlockState.Select].enabled = true;
        }
        
        void OnMouseExit()
        {
            sps[EBlockState.Select].enabled = false;
        }
    }
}