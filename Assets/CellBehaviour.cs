using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    private int _x;
    private int _y;
    private bool _occupied;
    private List<CellBehaviour> _adjacentCells;
    private BasePart _part;

    public void SetCoords(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void SetAdjacentTiles(List<CellBehaviour> adjacentTiles)
    {
        _adjacentCells = adjacentTiles;
    }

    public bool IsOccupied()
    {
        return _occupied;
    }

    public void SetOccupied(BasePart part)
    {
        if (_occupied && part != null)
            return;

        _occupied = part != null;
        _part = part;

        if (_occupied)
        {
            _part.GetComponent<BasePart>().Id = Guid.NewGuid().ToString().Substring(0, 4);
            // Connect new part with adjacent parts
            Debug.Log($"Placing part {_part.Id}");
            foreach (var p in GetAdjacentParts())
            {
                // Producer can't connect to more than one pipe
                if (p.GetPartType() == PartType.Producer && p.PipeConnections() > 0)
                    continue;

                // Pipe can only connect to two other things
                if (p.GetPartType() == PartType.Pipe && p.TotalConnections() > 1)
                    continue;

                // Consumer
                /*if (p.GetPartType() == PartType.Consumer && p.TotalConnections() > 1)
                    continue;*/

                p.ConnectPart(part);
            }
        }
    }

    private List<BasePart> GetAdjacentParts()
    {
        return _adjacentCells
            .Select(t => t._part)
            .Where(t => t != null)
            .ToList();
    }
}
