using System;
using System.Collections.Generic;
using System.Linq;

namespace WarChess
{
    /// <summary>
    /// Breadth first search
    /// </summary>
    public class BFS
    {
        public class Point
        {
            public int rowIndex;
            public int colIndex;
            public Point parent;
            public Point(int row, int col, Point p = null)
            {
                rowIndex = row;
                colIndex = col;
                parent = p;
            }
        }

        public int rowCount;
        public int colCount;

        /// <summary>
        /// Saves the points found, key is the point's index in string format, value is the point
        /// </summary>
        public Dictionary<string, Point> founds;
        
        public Func<int, int, bool> checkCondition;

        public BFS(int row, int col)
        {
            rowCount = row;
            colCount = col;
            founds = new Dictionary<string, Point>();
            checkCondition = (r, c) =>
            {
                bool result = GameApp.MapManager.GetBlockType(r, c) != EBlockType.Obstacle;
                return result;
            };
        }
        
        /// <summary>
        /// Search for the movable area
        /// </summary>
        /// <param name="row">Row of starting point</param>
        /// <param name="col">Column of starting point</param>
        /// <param name="step">Depth</param>
        /// <returns></returns>
        public List<Point> Search(int row, int col, int step)
        {
            // Defines the list found
            List<Point> searches = new List<Point>();
            Point start = new Point(row, col);
            searches.Add(start);
            founds.Add($"{row}_{col}", start);

            for (int i = 0; i < step; i++)
            {
                // Defines the list of points found and satisfying the condition in this round
                List<Point> temps = new List<Point>();
                
                // Loop through all the points found in the last round
                for (int j = 0; j < searches.Count; j++)
                {
                    Point current = searches[j];
                    FindSurroundingPoints(current, temps);
                }
                
                if (temps.Count == 0)
                {
                    break;
                }
                
                searches.Clear();
                searches.AddRange(temps);
            }
            
            return founds.Values.ToList();
        }

        /// <summary>
        /// Find the surrounding points of the current point
        /// </summary>
        /// <param name="current"></param>
        /// <param name="temps"></param>
        public void FindSurroundingPoints(Point current, List<Point> temps)
        {
            // up
            if (current.rowIndex > 0)
            {
                TryAddFinds(current.rowIndex - 1, current.colIndex, current, temps);
            }
            // down
            if (current.rowIndex < rowCount - 1)
            {
                TryAddFinds(current.rowIndex + 1, current.colIndex, current, temps);
            }
            // left
            if (current.colIndex > 0)
            {
                TryAddFinds(current.rowIndex, current.colIndex - 1, current, temps);
            }
            // right
            if (current.colIndex < colCount - 1)
            {
                TryAddFinds(current.rowIndex, current.colIndex + 1, current, temps);
            }
        }

        /// <summary>
        /// Add point to the founds list
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="parent"></param>
        /// <param name="temps"></param>
        public void TryAddFinds(int row, int col, Point parent, List<Point> temps)
        {
            // If the point is not in the founds list, and the point is not an obstacle, add it to the founds list
            bool condition = checkCondition?.Invoke(row, col) ?? true;
            if (!founds.ContainsKey($"{row}_{col}") && condition)
            {
                Point p = new Point(row, col, parent);
                temps.Add(p);
                founds.Add($"{row}_{col}", p);
            }
        }
    }
}