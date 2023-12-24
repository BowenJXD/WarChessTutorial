using System;
using UnityEngine;
using UnityEngine.UI;

namespace WarChess
{
    public static class Util
    {
        public static void SetIcon(this Image img, string res)
        {
            img.sprite = Resources.Load<Sprite>($"Icon/{res}");
        }

        /// <summary>
        /// Detect if mouse is over a 2d collider
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="mousePos"></param>
        /// <param name="callback"></param>
        public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, Action<Collider2D> callback)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
            callback?.Invoke(col);
        }
    }
}