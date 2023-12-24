using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    public class Singleton<T>
    {
        private static readonly T instance = Activator.CreateInstance<T>();
        
        public static T Instance
        {
            get { return instance; }
        }

        public virtual void Init()
        {
            
        }

        public virtual void Update(float dt)
        {
            
        }

        public virtual void OnDestroy()
        {
            
        }
    }
}