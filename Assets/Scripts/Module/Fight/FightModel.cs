using System.Collections.Generic;

namespace WarChess
{
    public class OptionData
    {
        public int id;
        public string eventName;
        public string name;
    }
    
    /// <summary>
    /// Data model for fight scene
    /// </summary>
    public class FightModel : BaseModel
    {
        public List<OptionData> options;
        public ConfigData optionConfig;

        public FightModel(BaseController ctl) : base(ctl)
        {
            options = new List<OptionData>();
        }

        public override void Init()
        {
            base.Init();
            optionConfig = GameApp.ConfigManager.GetConfigData("option");
            foreach (var item in optionConfig.GetLines())
            {
                OptionData data = new OptionData();
                data.id = int.Parse(item.Value["Id"]);
                data.eventName = item.Value["EventName"];
                data.name = item.Value["Name"];
                options.Add(data);
            }
        }
    }
}