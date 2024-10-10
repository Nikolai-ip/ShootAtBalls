using UnityEngine;

namespace GameCore.Field
{
     public class Grid
     {
          private int _columns;
          private int _rows;
          private Vector2[][] _grid;
          public int Length => _grid.Length;
          public Grid(int rows, int columns,Vector2 startPos,Vector2 cellSize) 
          {
               CreateGrid(rows, columns,startPos,cellSize);
          }
          private void CreateGrid(int rows, int columns, Vector2 startPos,Vector2 cellSize)
          {
               _grid = new Vector2[rows][];
               for (int i = 0; i < rows; i++)
               {
                    _grid[i] = new Vector2[columns];
                    for (int j = 0; j < columns; j++)
                    {
                         float x = j * cellSize.x;
                         if (i % 2 == 0) x += cellSize.x / 2;
                         float y = -i * (cellSize.y-cellSize.y*Mathf.Sqrt(3)/12);
                         _grid[i][j] = startPos + new Vector2(x, y);
                    }
               }
          }

          public Vector2 this[int i, int j] => _grid[i][j];
          public Vector2[] this[int i] => _grid[i];


     }
}
