using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverlayTileGenerator : MonoBehaviour
{
    public int charaJumpHeight;
    private static OverlayTileGenerator _instance;

    public static OverlayTileGenerator Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    public Dictionary <Vector2Int, OverlayTile> map;

    private void Awake()
    {
        if ( _instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();
        BoundsInt bounds = tileMap.cellBounds;

        for (int z = bounds.max.z; z > bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);

                    if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, (float)cellWorldPosition.z + 0.5f);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                        overlayTile.gridLocation = tileLocation;
                        
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }

    public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile)
    {
        var map = OverlayTileGenerator.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //Top
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight && map[locationToCheck].isBlocked != true)
                neighbours.Add(map[locationToCheck]);
        }

        //Bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            if(Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight && map[locationToCheck].isBlocked != true)
               neighbours.Add(map[locationToCheck]);
        }

        //Right
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight && map[locationToCheck].isBlocked != true)
                neighbours.Add(map[locationToCheck]);
        }

        //Left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight && map[locationToCheck].isBlocked != true)
                neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }

    public List<OverlayTile> GetAttackRangeTiles(OverlayTile currentOverlayTile)
    {
        var map = OverlayTileGenerator.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //Top
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight)
                neighbours.Add(map[locationToCheck]);
        }

        //Bottom
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight)
                neighbours.Add(map[locationToCheck]);
        }

        //Right
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight)
                neighbours.Add(map[locationToCheck]);
        }

        //Left
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
            );

        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= charaJumpHeight)
                neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}
