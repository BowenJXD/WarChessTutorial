using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Enemy description view
    /// </summary>
    public class EnemyDesView : BaseView
    {
        public override void Open(params object[] args)
        {
            base.Open(args);
            Enemy enemy = args[0] as Enemy;
            
            if (enemy != null)
            {
                Find<Image>("bg/icon").SetIcon($"{enemy.data["Icon"]}");
                Find<Text>("bg/atkTxt/txt").text = $"{enemy.attack}";
                Find<Text>("bg/StepTxt/txt").text = $"{enemy.step}";
                Find<Image>("bg/hp/fill").fillAmount = (float)enemy.curHp/enemy.maxHp;
                Find<Text>("bg/hp/txt").text = $"{enemy.curHp}/{enemy.maxHp}";
            }
        }
    }
}