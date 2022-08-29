using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AStarManager
{

    private OverlayTile[,] tiles;

    private List<OverlayTile> openList;
    private List<OverlayTile> closeList;
    private RangeFinder rangeFinder;

    public float charaJumpHeight = 1;


    public List<OverlayTile> FindPath (OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closeList = new List<OverlayTile>();

        //List<OverlayTile> inRangeTiles = rangeFinder.GetTilesInRange(start, range);

        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closeList.Add(currentOverlayTile);

            if(currentOverlayTile == end)
            {
                //finalize
                return GetFinishedList(start, end);
            }

            var neighbourTiles = OverlayTileGenerator.Instance.GetNeighbourTiles(currentOverlayTile);

            foreach(var neighbour in neighbourTiles)
            {
                if(neighbour.isBlocked || closeList.Contains(neighbour))
                {
                    continue;
                }

                neighbour.G = GetManhattenDistance(start, neighbour);
                neighbour.H = GetManhattenDistance(end, neighbour);

                neighbour.previous = currentOverlayTile;

                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }

        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;

        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    
}
