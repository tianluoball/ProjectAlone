using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTranslator
{
    public enum ArrowDirection
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        TopRight = 5, 
        BottomRight = 6,
        TopLeft = 7,
        BottomLeft = 8,
        UpFinished = 9,
        DownFinished = 10,
        LeftFinished = 11,
        RightFinished = 12
    }

    public ArrowDirection TranslateDirection(OverlayTile previousTile, OverlayTile currentTile, OverlayTile nextTile)
    {
        bool isFinal = nextTile == null;

        Vector2Int pastDirection = previousTile != null ? currentTile.grid2DLocation - previousTile.grid2DLocation : new Vector2Int(0, 0);
        Vector2Int nextDirection = nextTile != null ? nextTile.grid2DLocation - currentTile.grid2DLocation : new Vector2Int(0, 0);
        Vector2Int direction = pastDirection != nextDirection ? pastDirection + nextDirection : nextDirection;

        if(direction == new Vector2Int(0,1) && !isFinal)
        {
            return ArrowDirection.Up;
        }

        if (direction == new Vector2Int(0, -1) && !isFinal)
        {
            return ArrowDirection.Down;
        }

        if (direction == new Vector2Int(1, 0) && !isFinal)
        {
            return ArrowDirection.Right;
        }

        if (direction == new Vector2Int(-1, 0) && !isFinal)
        {
            return ArrowDirection.Left;
        }

        if(direction == new Vector2Int(1, 1))
        {
            if(pastDirection.y < nextDirection.y) { return ArrowDirection.BottomLeft; }
            else { return ArrowDirection.TopRight; }
        }
        if (direction == new Vector2Int(-1, 1))
        {
            if (pastDirection.y < nextDirection.y) { return ArrowDirection.BottomRight; }
            else { return ArrowDirection.TopLeft; }
        }
        if (direction == new Vector2Int(1, -1))
        {
            if (pastDirection.y > nextDirection.y) { return ArrowDirection.TopLeft; }
            else { return ArrowDirection.BottomRight; }
        }
        if (direction == new Vector2Int(-1, -1))
        {
            if (pastDirection.y > nextDirection.y) { return ArrowDirection.TopRight; }
            else { return ArrowDirection.BottomLeft; }
        }

        if (direction == new Vector2Int(0, 1) && isFinal)
        {
            return ArrowDirection.UpFinished;
        }

        if (direction == new Vector2Int(0, -1) && isFinal)
        {
            return ArrowDirection.DownFinished;
        }

        if (direction == new Vector2Int(1, 0) && isFinal)
        {
            return ArrowDirection.RightFinished;
        }

        if (direction == new Vector2Int(-1, 0) && isFinal)
        {
            return ArrowDirection.LeftFinished;
        }

        return ArrowDirection.None;
    }
}
