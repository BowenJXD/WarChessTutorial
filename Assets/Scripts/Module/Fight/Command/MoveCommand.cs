using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    ///  Move command
    /// </summary>
    public class MoveCommand : BaseCommand
    {
        protected List<AStar.Point> paths;
        protected AStar.Point curPoint;
        protected int pathIndex;

        protected const float MOVE_SPEED = 5;

        /// <summary>
        /// Previous row index, used to be undone
        /// </summary>
        protected int preRowIndex;
        
        /// <summary>
        /// Previous column index, used to be undone
        /// </summary>
        protected int preColIndex;
        
        public MoveCommand(ModelBase newModel) : base(newModel)
        {
            
        }
        
        public MoveCommand(ModelBase model, List<AStar.Point> paths) : base(model)
        {
            this.paths = paths;
            pathIndex = 0;
        }

        public override void Do()
        {
            base.Do();
            preRowIndex = model.rowIndex;
            preColIndex = model.colIndex;
            // Change the block type of the previous position to null
            GameApp.MapManager.ChangeBlockType(model.rowIndex, model.colIndex, EBlockType.Null);
        }

        public override bool OnUpdate(float dt)
        {
            curPoint = paths[pathIndex];
            if (model.Move(curPoint.x, curPoint.y, dt * MOVE_SPEED))
            {
                pathIndex++;
                if (pathIndex >= paths.Count)
                {
                    // Arrived at the destination
                    model.PlayAni("idle");
                    // Change the block type of the current position to obstacle
                    GameApp.MapManager.ChangeBlockType(model.rowIndex, model.colIndex, EBlockType.Obstacle);
                    
                    // Opens the option panel
                    GameApp.ViewManager.Open(EViewType.SelectOptionView, model.data["Event"], (Vector2)model.tf.position);
                    
                    return true;
                }
            }
            
            model.PlayAni("move");

            return false;
        }

        public override void Undo()
        {
            base.Undo();
            
            // back to the previous position
            Vector3 pos = GameApp.MapManager.GetBlockPos(preRowIndex, preColIndex);
            pos.z = model.tf.position.z;
            model.tf.position = pos;
            
            GameApp.MapManager.ChangeBlockType(model.rowIndex, model.colIndex, EBlockType.Null);
            model.rowIndex = preRowIndex;
            model.colIndex = preColIndex;
            GameApp.MapManager.ChangeBlockType(model.rowIndex, model.colIndex, EBlockType.Obstacle);
        }
    }
}