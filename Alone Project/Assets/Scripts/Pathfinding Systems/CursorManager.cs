using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static ArrowTranslator;

public class CursorManager : MonoBehaviour
{
    public GameObject characterPrefab;
    private AStarManager _astarManager;
    public OverlayTile charaTile;
    public OverlayTile cursorTile;
    private RangeFinder rangeFinder;
    private ArrowTranslator translator;
    private CharacterInfo character;

    public List<OverlayTile> finalPath;
    public List<OverlayTile> inRangeTiles = new List<OverlayTile>();
    public List<OverlayTile> attackRangeTiles = new List<OverlayTile>();

    public int moveRange;

    private bool isMoving = false;


    public void Start()
    {
        _astarManager = new AStarManager();
        rangeFinder = new RangeFinder();
        translator = new ArrowTranslator();
    }

    void Late()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (focusedTileHit.HasValue)
        {

            OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();

            transform.position = overlayTile.transform.position;

            if (inRangeTiles.Contains(overlayTile) && !isMoving)
            {
                finalPath = _astarManager.FindPath(charaTile, cursorTile);

                foreach(var tile in inRangeTiles)
                {
                    tile.SetArrowSprite(ArrowDirection.None);
                }

                for(int i = 0; i < finalPath.Count; i++)
                {
                    var previousTile = i > 0 ? finalPath[i - 1] : charaTile;
                    var nextTile = i < finalPath.Count - 1 ? finalPath[i + 1] : null;

                    var arrowDirection = translator.TranslateDirection(previousTile, finalPath[i], nextTile);
                    finalPath[i].SetArrowSprite(arrowDirection);
                }
            }
        }
        return;
    }

    public void ShowArrow()
    {

        OverlayTile overlayTile = cursorTile;

            transform.position = overlayTile.transform.position;

            if (inRangeTiles.Contains(overlayTile) && !isMoving)
            {
                finalPath = _astarManager.FindPath(charaTile, cursorTile);

                foreach (var tile in inRangeTiles)
                {
                    tile.SetArrowSprite(ArrowDirection.None);
                }

                for (int i = 0; i < finalPath.Count; i++)
                {
                    var previousTile = i > 0 ? finalPath[i - 1] : charaTile;
                    var nextTile = i < finalPath.Count - 1 ? finalPath[i + 1] : null;

                    var arrowDirection = translator.TranslateDirection(previousTile, finalPath[i], nextTile);
                    finalPath[i].SetArrowSprite(arrowDirection);
                }
            }

    }

    private RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);
        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }

    private void PositionCharacterOnTile(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        
        character.activeTile = tile;
    }

    public void GetInRangeTiles(OverlayTile characterTile, int range)
    {
        foreach (var tile in inRangeTiles)
        {
            tile.HideTile();
        }

        inRangeTiles = rangeFinder.GetTilesInRange(characterTile, range);

        foreach (var tile in inRangeTiles)
        {
            tile.ShowTile();
        }
    }

    public void GetInAttackRangeTiles(OverlayTile characterTile, int range)
    {
        foreach (var tile in attackRangeTiles)
        {
            tile.HideTile();
        }

        attackRangeTiles = rangeFinder.GetAttackInRange(characterTile, range);

        foreach (var tile in attackRangeTiles)
        {
            tile.ShowTile();
        }
    }
}
