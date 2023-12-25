using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// Skill related utils
    /// </summary>
    public static class SkillHelper
    {
        public static bool IsModelInSkillArea(this ISkillUser skillUser, ModelBase target)
        {
            BFS.Point point = new BFS.Point(target.rowIndex, target.colIndex);
            bool result = skillUser.SkillArea.Contains(point);
            return result;
        }

        /// <summary>
        /// Get the target of the skill:
        /// 0: target under mouse pointer
        /// 1: all targets in the skill area
        /// 2: all heroes in the skill area
        /// </summary>
        /// <param name="skillUser"></param>
        /// <returns></returns>
        public static List<ModelBase> GetTarget(this ISkillUser skillUser)
        {
            List<ModelBase> results = null;
            switch (skillUser.SkillPro.targetWay)
            {
                case ESkillTargetWay.Single:
                    results = GetTargetSingle(skillUser);
                    break;
                case ESkillTargetWay.All:
                    results = GetTargetAll(skillUser);
                    break;
                case ESkillTargetWay.Ally:
                    results = GetTargetAlly(skillUser);
                    break;
                case ESkillTargetWay.Opponent:
                    results = GetTargetOpponent(skillUser);
                    break;
            }

            return results;
        }

        public static List<ModelBase> GetTargetSingle(ISkillUser skillUser)
        {
            List<ModelBase> results = new List<ModelBase>();
            Collider2D col = Util.ScreenPointToRay2D();
            if (col != null)
            {
                ModelBase target = col.GetComponent<ModelBase>();
                if (target != null)
                {
                    if (skillUser.SkillPro.targetType == target.type)
                    {
                        results.Add(target);
                    }
                }
            }
            return results;
        }

        public static List<ModelBase> GetTargetAll(ISkillUser skillUser)
        {
            List<ModelBase> results = new List<ModelBase>();
            List<ModelBase> allModels = new List<ModelBase>();
            allModels.AddRange(GameApp.FightManager.heroes);
            allModels.AddRange(GameApp.FightManager.enemies);
            
            foreach (var model in allModels)
            {
                if (skillUser.IsModelInSkillArea(model))
                {
                    results.Add(model);
                }
            }

            return results;
        }
        
        public static List<ModelBase> GetTargetAlly(ISkillUser skillUser)
        {
            List<ModelBase> results = new List<ModelBase>();
            List<ModelBase> allModels = new List<ModelBase>();
            allModels.AddRange(skillUser.SkillPro.targetType == EModelType.Hero ? GameApp.FightManager.heroes : GameApp.FightManager.enemies);
            
            foreach (var model in allModels)
            {
                if (skillUser.IsModelInSkillArea(model))
                {
                    results.Add(model);
                }
            }

            return results;
        }
        
        public static List<ModelBase> GetTargetOpponent(ISkillUser skillUser)
        {
            List<ModelBase> results = new List<ModelBase>();
            List<ModelBase> allModels = new List<ModelBase>();
            allModels.AddRange(skillUser.SkillPro.targetType == EModelType.Enemy ? GameApp.FightManager.heroes : GameApp.FightManager.enemies);
            
            foreach (var model in allModels)
            {
                if (skillUser.IsModelInSkillArea(model))
                {
                    results.Add(model);
                }
            }

            return results;
        }
    }
}