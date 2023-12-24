using System;
using System.Collections.Generic;

namespace WarChess
{
    public class GameTimer
    {
        /// <summary>
        /// All timers
        /// </summary>
        protected List<GameTimerData> timers;

        public GameTimer()
        {
            timers = new List<GameTimerData>();
        }
        
        public void RegisterTimer(float timer, Action callback)
        {
            timers.Add(new GameTimerData(timer, callback));
        }
        
        public void OnUpdate(float dt)
        {
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                if (timers[i].OnUpdate(dt))
                {
                    timers.RemoveAt(i);
                }
            }
        }

        public void Break()
        {
            timers.Clear();
        }
        
        public int Count()
        {
            return timers.Count;
        }
    }
}