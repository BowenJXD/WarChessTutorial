using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WarChess
{
    [CanEditMultipleObjects, CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Snap Position"))
            {
                Tilemap tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
                
                var allPos = tilemap.cellBounds.allPositionsWithin;
                int minX = 0;
                int minY = 0;
                
                if (allPos.MoveNext())
                {
                    Vector3Int curPos = allPos.Current;
                    minX = curPos.x;
                    minY = curPos.y;
                }
                
                Enemy enemy = target as Enemy;
                if (enemy != null)
                {
                    Vector3Int cellPos = tilemap.WorldToCell(enemy.transform.position);
                    enemy.rowIndex = Mathf.Abs(minY - cellPos.y);
                    enemy.colIndex = Mathf.Abs(minX - cellPos.x);
                    enemy.transform.position = tilemap.CellToWorld(cellPos) + tilemap.layoutGrid.cellSize / 2 + Vector3.back;
                }
                
            }
        }
    }
}