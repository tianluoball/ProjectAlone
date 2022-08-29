using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder
{
    public List<OverlayTile> GetTilesInRange(OverlayTile startTile, int range)
    {
        var inRangeTiles = new List<OverlayTile>();
        int stepCount = 0;

        inRangeTiles.Add(startTile);

        var tileForPreviousStep = new List <OverlayTile>();

        tileForPreviousStep.Add(startTile);

        while(stepCount < range)
        {
            var surroundingTiles = new List<OverlayTile>();

            foreach(var item in tileForPreviousStep)
            {
                surroundingTiles.AddRange(OverlayTileGenerator.Instance.GetNeighbourTiles(item));
            }

            inRangeTiles.AddRange(surroundingTiles);
            tileForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }

    public List<OverlayTile> GetAttackInRange(OverlayTile startTile, int range)
    {
        var inRangeTiles = new List<OverlayTile>();
        int stepCount = 0;

        inRangeTiles.Add(startTile);

        var tileForPreviousStep = new List<OverlayTile>();

        tileForPreviousStep.Add(startTile);

        while (stepCount < range)
        {
            var surroundingTiles = new List<OverlayTile>();

            foreach (var item in tileForPreviousStep)
            {
                surroundingTiles.AddRange(OverlayTileGenerator.Instance.GetAttackRangeTiles(item));
            }

            inRangeTiles.AddRange(surroundingTiles);
            tileForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }
}
