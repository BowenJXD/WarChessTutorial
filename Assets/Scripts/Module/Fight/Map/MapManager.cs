using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace WarChess
{
    /// <summary>
    /// Saves block data
    /// </summary>
    public class MapManager
    {
        protected Tilemap tilemap;

        public Block[,] mapArr;

        public int rowCount;
        public int colCount;
        
        public List<Sprite> dirSpArr;

        public void Init()
        {
            dirSpArr = new List<Sprite>();
            
            for (int i = 0; i < (int)EBlockDirection.max; i++)
            {
                string path = $"Icon/{(EBlockDirection)i}";
                Sprite sp = Resources.Load<Sprite>(path);
                dirSpArr.Add(sp);
            }
            
            tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
            rowCount = 12;
            colCount = 20;
            
            mapArr = new Block[rowCount, colCount];

            // Saves every block's position in the map temporarily
            List<Vector3Int> tempPosArr = new List<Vector3Int>();
            
            // Loop through all the tiles in the tilemap
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                {
                    tempPosArr.Add(pos);
                }
            }
            
            // Convert tempPosArr to mapArr
            Object prefabObj = Resources.Load("Model/block");
            for (int i = 0; i < tempPosArr.Count; i++)
            {
                int row = i / colCount;
                int col = i % colCount;
                Block b = (Object.Instantiate(prefabObj) as GameObject)?.AddComponent<Block>();
                if (b != null)
                {
                    b.rowIndex = row;
                    b.colIndex = col;
                    Vector3 blockSize = tilemap.layoutGrid.cellSize;
                    b.transform.position = tilemap.CellToWorld(tempPosArr[i]) + blockSize / 2;
                    b.transform.SetParent(tilemap.transform);
                    mapArr[row, col] = b;
                }
                else
                {
                    Debug.LogError($"Failed to instantiate block at ({row}, {col})");
                }
            }
        }

        public Vector3 GetBlockPos(int row, int col)
        {
            return mapArr[row, col].transform.position;
        }
        
        public EBlockType GetBlockType(int row, int col)
        {
            return mapArr[row, col].type;
        }
        
        public EGridState GetGridState(int row, int col)
        {
            return mapArr[row, col].GridState;
        }
        
        public void ChangeBlockType(int row, int col, EBlockType type)
        {
            mapArr[row, col].type = type;
        }

        /// <summary>
        /// Show movable area
        /// </summary>
        /// <param name="model"></param>
        /// <param name="step"></param>
        /// <param name="color"></param>
        /// <param name="inaccessibleTypes"></param>
        public List<BFS.Point> ShowStepGrid(ModelBase model, int step, Color color, List<EBlockType> inaccessibleTypes = null)
        {
            BFS bfs = new BFS(rowCount, colCount, inaccessibleTypes);
            
            var points = bfs.Search(model.rowIndex, model.colIndex, step);

            foreach (var point in points)
            {
                mapArr[point.rowIndex, point.colIndex].GridState = EGridState.Movable;
            }

            return points;
        }
        
        public void HideStepGrid(ModelBase model, int step, List<EBlockType> inaccessibleTypes = null)
        {
            BFS bfs = new BFS(rowCount, colCount, inaccessibleTypes);
            
            var points = bfs.Search(model.rowIndex, model.colIndex, step);

            foreach (var point in points)
            {
                mapArr[point.rowIndex, point.colIndex].GridState = EGridState.None;
            }
        }

        /// <summary>
        /// Set the block's direction sprite and color depending on the direction enum
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="dir"></param>
        /// <param name="color"></param>
        public void SetBlockDir(int rowIndex, int colIndex, EBlockDirection dir, Color color)
        {
            mapArr[rowIndex, colIndex].SetDirSp(dirSpArr[(int)dir], color);
        }

        /*public void ShowAttackStep(ModelBase model, int attackStep, Color color)
        {
            int minRow = Mathf.Max(model.rowIndex - attackStep, 0);
            int minCol = Mathf.Max(model.colIndex - attackStep, 0);
            int maxRow = Mathf.Min(model.rowIndex + attackStep, rowCount - 1);
            int maxCol = Mathf.Min(model.colIndex + attackStep, colCount - 1);
            
            for (int i = minRow; i <= maxRow; i++)
            {
                for (int j = minCol; j <= maxCol; j++)
                {
                    if (i == model.rowIndex && j == model.colIndex) continue;
                    
                    mapArr[i, j].ShowGrid(color);
                }
            }
        }
        
        public void HideAttackStep(ModelBase model, int attackStep)
        {
            
        }*/
        
        /// <summary>
        ///  Get the direction of the block via the start and next point
        /// </summary>
        /// <param name="start"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public EBlockDirection GetDirectionAtStart(AStar.Point start, AStar.Point next)
        {
            int rowOffset = next.x - start.x;
            int colOffset = next.y - start.y;
            if (rowOffset == 0)
            {
                return EBlockDirection.horizontal;
            }
            else if (colOffset == 0)
            {
                return EBlockDirection.vertical;
            }

            return EBlockDirection.none;
        }

        /// <summary>
        ///  Get the direction of the block via the end and previous point
        /// </summary>
        /// <param name="end"></param>
        /// <param name="pre"></param>
        /// <returns></returns>
        public EBlockDirection GetDirectionAtEnd(AStar.Point end, AStar.Point pre)
        {
            int rowOffset = end.x - pre.x;
            int colOffset = end.y - pre.y;
            if (rowOffset == 0 && colOffset > 0)
            {
                return EBlockDirection.right;
            }
            else if (rowOffset == 0 && colOffset < 0)
            {
                return EBlockDirection.left;
            }
            else if (rowOffset > 0 && colOffset == 0)
            {
                return EBlockDirection.up;
            }
            else if (rowOffset < 0 && colOffset == 0)
            {
                return EBlockDirection.down;
            }

            return EBlockDirection.none;
        }

        /// <summary>
        ///  Get the direction of the block via the previous, current point and end point
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="current"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public EBlockDirection GetDirectionAtCurrent(AStar.Point pre, AStar.Point current, AStar.Point end)
        {
            EBlockDirection result = EBlockDirection.none;
            
            int preRowOffset = pre.x - current.x;
            int preColOffset = pre.y - current.y;
            int endRowOffset = end.x - current.x;
            int endColOffset = end.y - current.y;
            
            int sumRowOffset = preRowOffset + endRowOffset;
            int sumColOffset = preColOffset + endColOffset;

            if (sumRowOffset == 1 && sumColOffset == -1)
            {
                result = EBlockDirection.left_up;
            }
            else if (sumRowOffset == 1 && sumColOffset == 1)
            {
                result = EBlockDirection.right_up;
            }
            else if (sumRowOffset == -1 && sumColOffset == -1)
            {
                result = EBlockDirection.left_down;
            }
            else if (sumRowOffset == -1 && sumColOffset == 1)
            {
                result = EBlockDirection.right_down;
            }
            else
            {
                if (preRowOffset == 0)
                {
                    result = EBlockDirection.horizontal;
                }
                else
                {
                    result = EBlockDirection.vertical;
                }
            }

            return result;
        }
    }
}