using System.Collections.Generic;
using System.Linq;
using GameCore.Projectile;
using StaticData;
using UnityEngine;

namespace GameCore.Field
{
    public class BubbleField:MonoBehaviour
    {   
        private Grid _grid;
        [SerializeField] private CircleCollider2D _bubbleCollider;
        [SerializeField] private List<Bubble> _bubbles;
        private Dictionary<char, Bubble> _bubbleNames;
        public void Init(BubbleFieldData data)
        {
            string field = data.Field;
            int columns = GetCountOfColumns(field);
            int rows = GetCountOfRows(field);
            float radius = _bubbleCollider.radius;
            _grid = new Grid(rows,columns,GetStartPos(), new Vector2(radius,radius));
            _bubbleNames = _bubbles.ToDictionary(value => char.ToLower(value.name.First()));
            CreateBubbles(field.Replace("\n", ""), columns,rows);
        }
        
        private int GetCountOfRows(string field) => field.Count(c => c.Equals('\n'))+1;
        private int GetCountOfColumns(string field) => field.TakeWhile(c => !c.Equals('\n')).Count();

        private Vector2 GetStartPos()
        {
            Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            float leftBoundary = screenBottomLeft.x;
            float topBoundary = screenTopRight.y;
            var radius = _bubbleCollider.radius;
            var startPos = new Vector2(leftBoundary + radius, topBoundary - radius);
            return startPos;
        }

        private void CreateBubbles(string field, int columns,int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    var symbol = field[index];
                    if (_bubbleNames.TryGetValue(symbol, out Bubble prefab))
                    {
                        var bubble = Instantiate(prefab,transform);
                        var pos = _grid[i][j];
                        bubble.transform.position = pos;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_grid == null) return;
            for (int i = 0; i < _grid.Length; i++)
            {
                for (int j = 0; j < _grid[i].Length; j++)
                {
                    Gizmos.DrawWireSphere(_grid[i][j],0.1f);
                }
            }
        }
    }
}