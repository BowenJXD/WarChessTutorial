using System.Collections.Generic;

namespace WarChess
{
    public class LevelData
    {
        public int id;
        public string name;
        public string sceneName;
        public string description;
        public bool isPassed; // whether the level is passed 

        public LevelData(Dictionary<string, string> data)
        {
            id = int.Parse(data["Id"]);
            name = data["Name"];
            sceneName = data["SceneName"];
            description = data["Des"];
        }
    }
    
    /// <summary>
    /// Level data model
    /// </summary>
    public class LevelModel : BaseModel
    {
        protected ConfigData levelConfig;

        protected Dictionary<int, LevelData> levels;

        public LevelData current;

        public LevelModel()
        {
            levels = new Dictionary<int, LevelData>();
        }

        public override void Init()
        {
            levelConfig = GameApp.ConfigManager.GetConfigData("level");
            foreach (var item in levelConfig.GetLines())
            {
                LevelData levelData = new LevelData(item.Value);
                levels.Add(levelData.id, levelData);
            }
        }
        
        public LevelData GetLevelData(int id)
        {
            if (levels.ContainsKey(id))
            {
                return levels[id];
            }
            else
            {
                return null;
            }
        }
    }
}