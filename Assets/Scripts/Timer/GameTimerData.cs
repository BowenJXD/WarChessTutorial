using System;

namespace WarChess
{
    public class GameTimerData
    {
        /// <summary>
        /// Duration
        /// </summary>
        protected float timer;
        protected Action callback;
        
        public GameTimerData(float timer, Action callback)
        {
            this.timer = timer;
            this.callback = callback;
        }
        
        public bool OnUpdate(float dt)
        {
            timer -= dt;
            if (timer <= 0)
            {
                callback?.Invoke();
                return true;
            }

            return false;
        }
    }
}