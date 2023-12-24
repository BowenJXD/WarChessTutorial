using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Hero description view
    /// </summary>
    public class HeroDesView : BaseView
    {
        public override void Open(params object[] args)
        {
            base.Open(args);
            Hero hero = args[0] as Hero;
            if (hero != null)
            {
                Find<Image>("bg/icon").SetIcon($"{hero.data["Icon"]}");
                Find<Text>("bg/atkTxt/txt").text = $"{hero.attack}";
                Find<Text>("bg/StepTxt/txt").text = $"{hero.step}";
                Find<Image>("bg/hp/fill").fillAmount = (float)hero.curHp/hero.maxHp;
                Find<Text>("bg/hp/txt").text = $"{hero.curHp}/{hero.maxHp}";
            }
        }
    }
}