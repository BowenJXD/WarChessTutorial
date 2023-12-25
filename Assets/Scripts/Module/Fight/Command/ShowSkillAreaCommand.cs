using UnityEngine;

namespace WarChess
{
    public class ShowSkillAreaCommand : BaseCommand
    {
        protected ISkillUser skillUser;
        
        public ShowSkillAreaCommand(ModelBase newModel) : base(newModel)
        {
            skillUser = model as ISkillUser;
        }

        public override void Do()
        {
            base.Do();
            skillUser?.ShowSkillArea();
            GameApp.MessageCenter.AddEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        void OnUnselectCallback(object arg)
        {
            skillUser?.HideSkillArea();
            skillUser = null;
        }
        
        public override bool OnUpdate(float dt)
        {
            return skillUser == null;
        }
    }
}