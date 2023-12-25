using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    public class Hero : ModelBase, ISkillUser
    {
        public SkillProperty SkillPro { get; set; }
        public List<BFS.Point> SkillArea { get; set; }

        public void Init(Dictionary<string, string> initData, int row, int col)
        {
            data = initData;
            rowIndex = row;
            colIndex = col;
            id = int.Parse(data["Id"]);
            step = int.Parse(data["Step"]);
            attack = int.Parse(data["Attack"]);
            type = (EModelType) int.Parse(data["Type"]);
            maxHp = int.Parse(data["Hp"]);
            curHp = maxHp;
            SkillPro = new SkillProperty(int.Parse(data["Skill"]));
        }
        
        protected override void OnSelectCallback(object arg)
        {
            // Can only select the hero when it's the player's turn
            if (GameApp.FightManager.state == EFightState.Player)
            {
                if (GameApp.CommandManager.IsRunningCommand) return;
                
                GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);

                if (!IsStop)
                {
                    GameApp.MapManager.ShowStepGrid(this, step, Color.green);
                    GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                    AddOptionEvents();
                }
                
                GameApp.ViewManager.Open(EViewType.HeroDesView, this);
            }
        }

        void AddOptionEvents()
        {
            GameApp.MessageCenter.AddTempEvent(Defines.OnAttackEvent, OnAttackCallback);
            GameApp.MessageCenter.AddTempEvent(Defines.OnIdleEvent, OnIdleCallback);
            GameApp.MessageCenter.AddTempEvent(Defines.OnCancelEvent, OnCancelCallback);
        }

        void OnAttackCallback(object arg)
        {
            GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
        }

        void OnIdleCallback(object arg)
        {
            IsStop = true;
        }
        
        void OnCancelCallback(object arg)
        {
            GameApp.CommandManager.Undo();
        }

        protected override void OnUnselectCallback(object arg)
        {
            base.OnUnselectCallback(arg);
            GameApp.ViewManager.Close(EViewType.HeroDesView);
        }

        public void ShowSkillArea()
        {
            SkillArea = GameApp.MapManager.ShowStepGrid(this, SkillPro.attackRange, Color.red, new List<EBlockType>());
        }

        public void HideSkillArea()
        {
            GameApp.MapManager.HideStepGrid(this, SkillPro.attackRange, new List<EBlockType>());
            SkillArea = null;
        }
    }
}