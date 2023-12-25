using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    ///  A* algorithm
    /// </summary>
    public class AStar
    {
        public class Point
        {
            public int x;
            public int y;

            /// <summary>
            ///  g is the distance from the starting point to the current point
            /// </summary>
            public int g;

            /// <summary>
            ///  h is the distance from the current point to the end point
            /// </summary>
            public int h;

            /// <summary>
            ///  f = g + h
            /// </summary>
            public int f;

            /// <summary>
            ///  The parent point of the current point
            /// </summary>
            public Point parent;

            public Point(int x, int y, Point parent = null)
            {
                this.x = x;
                this.y = y;
                this.parent = parent;
            }

            /// <summary>
            /// Calculate the distance from the current point to the starting point
            /// </summary>
            /// <returns></returns>
            public int GetG()
            {
                int result = 0;
                Point currentParent = this.parent;
                while (currentParent != null)
                {
                    result++;
                    currentParent = currentParent.parent;
                }

                return result;
            }

            /// <summary>
            /// Calculate the distance from the current point to the end point
            /// </summary>
            /// <returns></returns>
            public int GetH(Point end)
            {
                return Mathf.Abs(end.x - x) + Mathf.Abs(end.y - y);
            }
        }

        protected int rowCount;
        protected int colCount;
        protected Point start;
        protected Point end;
        protected List<Point> openList;
        protected Dictionary<string, Point> closeDic;
        public Func<int, int, bool> checkCondition;

        public AStar(int row, int col)
        {
            rowCount = row;
            colCount = col;
            openList = new List<Point>();
            closeDic = new Dictionary<string, Point>();
            checkCondition = (r, c) =>
            {
                bool result = GameApp.MapManager.GetBlockType(r, c) != EBlockType.Obstacle;
                return result;
            };
        }

        /// <summary>
        /// A* algorithm:
        /// 1. Put the starting point into the open list
        /// 2. Find the point with the smallest f value in the open list,
        /// 3. Put it into the close list, and remove it from the open list
        /// 4. Put the (4) points around it into the open list
        /// 5. Check if the end point is in the open list, if not, repeat (2)
        /// 6. if yes, find the path
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="findCallback"></param>
        /// <returns></returns>
        public bool FindPath(Point startPoint, Point endPoint, Action<List<Point>> findCallback)
        {
            start = startPoint;
            end = endPoint;
            openList = new List<Point>();
            closeDic = new Dictionary<string, Point>();

            // 1. Put the starting point into the open list
            openList.Add(start);
            while (true)
            {
                Point current = GetMinFFromOpen();
                if (current == null)
                {
                    // No path found
                    return false;
                }
                else
                {
                    // 3. Put it into the close list, and remove it from the open list
                    openList.Remove(current);
                    closeDic.Add($"{current.x}_{current.y}", current);
                    // 4. Put the (4) points around it into the open list
                    AddAroundToOpen(current);
                    // 5. Check if the end point is in the open list, if not, repeat (2)
                    Point endInOpen = TryGetInOpen(endPoint.x, endPoint.y);
                    if (endInOpen != null)
                    {
                        // 6. if yes, find the path
                        List<Point> path = FindPath(endInOpen);
                        findCallback?.Invoke(path);
                        return true;
                    }

                    // Sort the open list by f value
                    openList.Sort(OpenSort);
                }
            }
        }

        public Point TryGetInOpen(int rowIndex, int colIndex)
        {
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].x == rowIndex && openList[i].y == colIndex)
                {
                    return openList[i];
                }
            }

            return null;
        }

        public Point TryGetInClose(int rowIndex, int colIndex)
        {
            string key = $"{rowIndex}_{colIndex}";
            if (closeDic.ContainsKey(key))
            {
                return closeDic[key];
            }

            return null;
        }

        public Point GetMinFFromOpen()
        {
            if (openList.Count == 0)
            {
                return null;
            }

            return openList[0];
        }

        /// <summary>
        ///  Add the (4) points around the current point to the open list
        /// </summary>
        /// <param name="current"></param>
        public void AddAroundToOpen(Point current)
        {
            // up
            if (current.x > 0)
            {
                AddToOpen(current.x - 1, current.y, current);
            }

            // down
            if (current.x < rowCount - 1)
            {
                AddToOpen(current.x + 1, current.y, current);
            }

            // left
            if (current.y > 0)
            {
                AddToOpen(current.x, current.y - 1, current);
            }

            // right
            if (current.y < colCount - 1)
            {
                AddToOpen(current.x, current.y + 1, current);
            }
        }

        /// <summary>
        ///  Add the point to the open list, if it is not in the open list or the close list
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="parent"></param>
        public void AddToOpen(int rowIndex, int colIndex, Point parent)
        {
            Point open = TryGetInOpen(rowIndex, colIndex);
            Point close = TryGetInClose(rowIndex, colIndex);
            if (open == null && close == null && checkCondition(rowIndex, colIndex))
            {
                Point point = new Point(rowIndex, colIndex, parent);
                point.g = point.GetG();
                point.h = point.GetH(end);
                point.f = point.g + point.h;
                openList.Add(point);
            }
        }

        public int OpenSort(Point a, Point b)
        {
            return a.f - b.f;
        }

        public List<Point> FindPath(Point startPoint)
        {
            List<Point> path = new List<Point>();
            path.Add(startPoint);
            Point currentParent = startPoint.parent;
            while (currentParent != null)
            {
                path.Add(currentParent);
                currentParent = currentParent.parent;
            }
            
            path.Reverse();
            return path;
        }
    }
}