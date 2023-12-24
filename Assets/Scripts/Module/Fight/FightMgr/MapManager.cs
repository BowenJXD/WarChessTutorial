using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        public void Init()
        {
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
        
        public EBlockType GetBlockType(int row, int col)
        {
            return mapArr[row, col].type;
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
        public void ShowStepGrid(ModelBase model, int step)
        {
            BFS bfs = new BFS(rowCount, colCount);
            
            var points = bfs.Search(model.rowIndex, model.colIndex, step);

            foreach (var point in points)
            {
                mapArr[point.rowIndex, point.colIndex].ShowGrid(Color.green);
            }
        }
        
        public void HideStepGrid(ModelBase model, int step)
        {
            BFS bfs = new BFS(rowCount, colCount);
            
            var points = bfs.Search(model.rowIndex, model.colIndex, step);

            foreach (var point in points)
            {
                mapArr[point.rowIndex, point.colIndex].HideGrid();
            }
        }
    }
}