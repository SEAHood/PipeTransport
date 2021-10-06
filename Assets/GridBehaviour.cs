using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GameObject CellPrefab;
    public int X;
    public int Y;

    private GameObject[,] _cells;

    void Start()
    {
        _cells = new GameObject[X, Y];
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (var y = 0; y < Y; y++)
        {
            for (var x = 0; x < X; x++)
            {
                var offsetX = x - (X / 2) + 0.5f;
                var offsetY = y - (Y / 2) + 0.5f;
                var cell = Instantiate(CellPrefab, new Vector3(offsetX, offsetY), Quaternion.identity, transform);
                cell.GetComponent<CellBehaviour>().SetCoords(x, y);
                _cells[x, y] = cell;
            }
        }

        // Setup adjacent cell data
        for (var y = 0; y < Y; y++)
        {
            for (var x = 0; x < X; x++)
            {
                var adjacentTiles = new List<CellBehaviour>();

                if (x > 0)
                    adjacentTiles.Add(_cells[x - 1, y].GetComponent<CellBehaviour>());
                if (y > 0)
                    adjacentTiles.Add(_cells[x, y - 1].GetComponent<CellBehaviour>());
                if (x < X - 1)
                    adjacentTiles.Add(_cells[x + 1, y].GetComponent<CellBehaviour>());
                if (y < Y - 1)
                    adjacentTiles.Add(_cells[x, y + 1].GetComponent<CellBehaviour>());

                _cells[x, y].GetComponent<CellBehaviour>().SetAdjacentTiles(adjacentTiles);
            }
        }
    }
}
