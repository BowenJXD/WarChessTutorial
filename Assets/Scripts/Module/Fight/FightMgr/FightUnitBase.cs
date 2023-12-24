namespace WarChess
{
    /// <summary>
    /// base class for fight unit
    /// </summary>
    public class FightUnitBase
    {
        public virtual void Init()
        {
            
        }

        public virtual bool OnUpdate(float delta)
        {
            return false;
        }
    }
}