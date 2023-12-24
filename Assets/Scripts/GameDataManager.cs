using System.Collections.Generic;

namespace WarChess
{
    /// <summary>
    /// Saves player's basic game info
    /// </summary>
    public class GameDataManager
    {
        public List<int> heroes;

        public int money;
        
        public GameDataManager()
        {
            heroes = new List<int>();
            
            // three default hero ids.
            heroes.Add(10001);
            heroes.Add(10002);
            heroes.Add(10003);
        }
    }
}