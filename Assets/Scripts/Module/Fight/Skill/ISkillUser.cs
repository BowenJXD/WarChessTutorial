using System.Collections.Generic;

namespace WarChess
{
    public interface ISkillUser
    {
        SkillProperty SkillPro { get; set; }
        
        List<BFS.Point> SkillArea { get; set; }

        void ShowSkillArea();
        
        void HideSkillArea();
    }
}