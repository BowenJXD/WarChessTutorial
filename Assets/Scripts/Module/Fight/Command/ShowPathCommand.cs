using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// Show the moving path
    /// </summary>
    public class ShowPathCommand : BaseCommand
    {
        protected Collider2D previousCol;
        protected Collider2D currentCol;
        protected AStar aStar;
        protected AStar.Point start;
        protected AStar.Point end;
        /// <summary>
        ///  The previous path list, used to clear the previous path
        /// </summary>
        protected List<AStar.Point> prePaths;

        public ShowPathCommand(ModelBase newModel) : base(newModel)
        {
            prePaths = new List<AStar.Point>();
            start = new AStar.Point(model.rowIndex, model.colIndex);
            aStar = new AStar(GameApp.MapManager.rowCount, GameApp.MapManager.colCount);
        }

        public override bool OnUpdate(float dt)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (prePaths.Count > 0 && model.step >= prePaths.Count - 1)
                {
                    GameApp.CommandManager.AddCommand(new MoveCommand(model, prePaths));
                }
                else
                {
                    GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
                }
                
                return true;
            }

            currentCol = Util.ScreenPointToRay2D();
            
            if (currentCol && currentCol != previousCol)
            {
                previousCol = currentCol;
                Block b = currentCol.GetComponent<Block>();
                if (b)
                {
                    // start pathfinding
                    end = new AStar.Point(b.rowIndex, b.colIndex);
                    aStar.FindPath(start, end, UpdatePath);
                }
                else
                {
                    ClearPath();
                }
            }

            return false;
        }

        public void ClearPath()
        {
            if (prePaths.Count > 0)
            {
                foreach (var p in prePaths)
                {
                    GameApp.MapManager.mapArr[p.x, p.y].HideDirSp();
                }

                prePaths.Clear();
            }
        }
        
        void UpdatePath(List<AStar.Point> paths)
        {
            ClearPath();
            
            if (paths.Count >= 2 && model.step >= paths.Count - 1)
            {
                for (int i = 0; i < paths.Count; i++)
                {
                    EBlockDirection dir = EBlockDirection.none;

                    if (i == 0)
                    {
                        dir = GameApp.MapManager.GetDirectionAtStart(paths[i], paths[i + 1]);
                    }
                    else if (i == paths.Count - 1)
                    {
                        dir = GameApp.MapManager.GetDirectionAtEnd(paths[i], paths[i - 1]);
                    }
                    else
                    {
                        dir = GameApp.MapManager.GetDirectionAtCurrent(paths[i - 1], paths[i], paths[i + 1]);
                    }
                
                    GameApp.MapManager.SetBlockDir(paths[i].x, paths[i].y, dir, Color.yellow);
                }
            }
            prePaths = paths;
        }
    }
}