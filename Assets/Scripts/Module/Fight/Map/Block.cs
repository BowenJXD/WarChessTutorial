using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// A block on the map's grid
    /// </summary>
    public class Block : MonoBehaviour
    {
        public int rowIndex;
        public int colIndex;
        public EBlockType type;
        protected EGridState gridState;
        
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
            gridState = EGridState.None;
        }

        private void OnEnable()
        {
            GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.AddEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        private void OnDisable()
        {
            GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.RemoveEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        void OnSelectCallback(object arg)
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
        }

        void OnUnselectCallback(object arg)
        {
            HideDirSp();
        }

        public EGridState GridState
        {
            get => gridState;
            set => SetGridState(value);
        }
        
        void SetGridState(EGridState state)
        {
            gridState = state;
            
            Color color = Color.white;
            switch (gridState)
            {
                case EGridState.None:
                    color = Color.clear;
                    break;
                case EGridState.Normal:
                    color = Color.white;
                    break;
                case EGridState.Movable:
                    color = Color.green;
                    break;
                case EGridState.Attackable:
                    color = Color.red;
                    break;
            }
            sps[EBlockState.Grid].color = color;
            
            if (gridState == EGridState.None)
            {
                sps[EBlockState.Grid].enabled = false;
            }
            else
            {
                sps[EBlockState.Grid].enabled = true;
            }
        }

        void OnMouseEnter()
        {
            sps[EBlockState.Select].enabled = true;
        }
        
        void OnMouseExit()
        {
            sps[EBlockState.Select].enabled = false;
        }

        /// <summary>
        /// Set the direction arrow
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="color"></param>
        public void SetDirSp(Sprite sp, Color color)
        {
            sps[EBlockState.Dir].sprite = sp;
            sps[EBlockState.Dir].color = color;
            sps[EBlockState.Dir].enabled = true;
        }
        
        public void HideDirSp()
        {
            sps[EBlockState.Dir].enabled = false;
        }
    }
}