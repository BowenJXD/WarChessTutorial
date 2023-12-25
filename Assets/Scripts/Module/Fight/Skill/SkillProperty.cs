using System.Collections.Generic;

namespace WarChess
{
    /// <summary>
    /// Skill's property
    /// </summary>
    public class SkillProperty
    {
        public int id;
        public string name;
        public string attack;
        public int attackCount;
        public int attackRange;
        /// <summary>
        /// The way to target
        /// </summary>
        public ESkillTargetWay targetWay;
        /// <summary>
        /// The type of the target (hero or enemy)
        /// </summary>
        public EModelType targetType;
        public string sound;
        public string aniName;
        /// <summary>
        /// Duration of the skill, in milliseconds
        /// </summary>
        public float duration;
        /// <summary>
        /// Time to attack, in milliseconds
        /// </summary>
        public float attackTime;
        public string attackEffect;

        public SkillProperty(int id)
        {
            Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("skill").GetDataById(id);
            id = int.Parse(data["Id"]);
            name = data["Name"];
            attack = data["Atk"];
            attackCount = int.Parse(data["AtkCount"]);
            attackRange = int.Parse(data["Range"]);
            targetWay = (ESkillTargetWay) int.Parse(data["Target"]);
            targetType = (EModelType) int.Parse(data["TargetType"]);
            sound = data["Sound"];
            aniName = data["AniName"];
            duration = float.Parse(data["Time"]) / 1000f;
            attackTime = float.Parse(data["AttackTime"]) / 1000f;
            attackEffect = data["AttackEffect"];
        }
    }
}