using System;
using UnityEngine;

namespace WarChess
{
    public class DestroyObj : MonoBehaviour
    {
        public float duration = 0;
        
        public void StartDestroy()
        {
            Destroy(gameObject, duration);
        }
    }
}