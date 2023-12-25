using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Process dragging of hero's icon
    /// </summary>
    public class HeroItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected Dictionary<string, string> data;

        void Start()
        {
            transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
        }

        public void Init(Dictionary<string, string> initData)
        {
            data = initData;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameApp.ViewManager.Open(EViewType.DragHeroView, data["Icon"]);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameApp.ViewManager.Close(EViewType.DragHeroView);
            
            // detect if the mouse is over any block
            Util.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, col =>
            {
                if (col != null)
                {
                    Block b = col.GetComponent<Block>();
                    if (b != null)
                    {
                        Destroy(gameObject);
                        GameApp.FightManager.AddHero(b, data);
                    }
                }
            });
        }
    }
}