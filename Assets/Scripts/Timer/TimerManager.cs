using System;

namespace WarChess
{
    /// <summary>
    /// A basic global timer manager
    /// </summary>
    public class TimerManager
    {
        protected GameTimer timer;
        
        public TimerManager()
        {
            timer = new GameTimer();
        }
        
        public void Register(float duration, Action callback)
        {
            timer.Register(duration, callback);
        }
        
        public void OnUpdate(float dt)
        {
            timer.OnUpdate(dt);
        }
    }
}