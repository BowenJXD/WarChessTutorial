using System;
using UnityEngine;
using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Icon view when being dragged
    /// </summary>
    public class DragHeroView : BaseView
    {
        void Update()
        {
            // only show when canvas is enabled
            if (!canvas.enabled)
            {
                return;
            }

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = worldPos;
        }

        public override void Open(params object[] args)
        {
            transform.GetComponent<Image>().SetIcon(args[0].ToString());
        }
    }
}