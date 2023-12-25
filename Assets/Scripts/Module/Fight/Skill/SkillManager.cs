using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    public class SkillManager
    {
        protected GameTimer timer;
        
        /// <summary>
        /// Skill: the skill used, targets: the targets of the skill, callback: the callback after the skill is used
        /// </summary>
        protected Queue<(ISkillUser skill, List<ModelBase> targets, Action callback)> skillQueue;

        public SkillManager()
        {
            timer = new GameTimer();
            skillQueue = new Queue<(ISkillUser skill, List<ModelBase> targets, Action callback)>();
        }
        
        public void AddSkill(ISkillUser skillUser, List<ModelBase> targets = null, Action callback = null)
        {
            skillQueue.Enqueue((skillUser, targets, callback));
        }

        public void UseSkill(ISkillUser skillUser, List<ModelBase> targets, Action callback)
        {
            ModelBase current = skillUser as ModelBase;

            if (targets.Count > 0)
            {
                current.LookAtModel(targets[0]);
            }
            current.PlaySound(skillUser.SkillPro.sound);
            current.PlayAni(skillUser.SkillPro.aniName);
            
            timer.Register(skillUser.SkillPro.attackTime, () =>
            {
                int atkCount = Mathf.Min(skillUser.SkillPro.attackCount, targets.Count);
                
                for (int i = 0; i < atkCount; i++)
                {
                    ModelBase target = targets[i];
                    target.PlayEffect(skillUser.SkillPro.attackEffect);
                    target.GetHit(skillUser);
                }
            });
            
            timer.Register(skillUser.SkillPro.duration, () =>
            {
                current.PlayAni("idle");
                callback?.Invoke();
            });
        }

        public void OnUpdate(float dt)
        {
            timer.OnUpdate(dt);
            if (timer.Count() == 0 && skillQueue.Count > 0)
            {
                var next = skillQueue.Dequeue();
                if (next.targets != null)
                {
                    UseSkill(next.skill, next.targets, next.callback);
                }
            }
        }

        public bool IsRunningSkill()
        {
            return timer.Count() > 0 || skillQueue.Count > 0;
        }

        public void Clear()
        {
            timer.Break();
            skillQueue.Clear();
        }
    }
}