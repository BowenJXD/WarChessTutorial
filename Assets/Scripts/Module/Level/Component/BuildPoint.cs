using System;
using UnityEngine;

namespace WarChess
{
    public class BuildPoint : MonoBehaviour
    {
        public int levelId;

        void OnTriggerEnter2D(Collider2D col)
        {
            GameApp.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, levelId);
        }

        void OnTriggerExit2D(Collider2D col)
        {
            GameApp.MessageCenter.PostEvent(Defines.HideLevelDesEvent, levelId);
        }
    }
}