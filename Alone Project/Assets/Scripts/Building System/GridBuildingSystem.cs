using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static TileTypeEnum;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap TempTilemap;

    public TileBase greenTile;
    public TileBase redTile;
    public TileBase greyTile;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, greyTile);
        tileBases.Add(TileType.Green, greenTile);
        tileBases.Add(TileType.Red, redTile);
    }

    private void Update()
    {
        if (!temp)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }

        if (!temp.placed)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos);
            prevPos = cellPos;
            FollowBuilding();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (temp.canBePlaced())
                {
                    temp.Place();
                    temp = null;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearArea();
                Destroy(temp.gameObject);
            }
        }
    }


    #endregion

    #region Tilemap Management

    #endregion

    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    public void SelectWithBuilding(GameObject building)
    {
        temp = building.GetComponent<Building>();
        BoundsInt areaTemp = building.GetComponent<Building>().area;
        
        GridBuildingSystem.current.ClearArea(areaTemp, GridBuildingSystem.current.mainTilemap);
        FollowBuilding();
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        SetTileBlock(area, TileType.White, tilemap);
    }

    private void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTileBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTileBlock(area, mainTilemap);
        foreach (var b in baseArray)
        {
            if (b != tileBases[TileType.White])
            {
                return false;
            }
        }

        return true;

    }

    public void TakeArea(BoundsInt area)
    {
        SetTileBlock(area, TileType.Empty, TempTilemap);
        SetTileBlock(area, TileType.Green, mainTilemap);
    }

    #endregion

    private static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTileBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] tileArray, TileType type)
    {
        for (int i = 0; i < tileArray.Length; i++)
        {
            tileArray[i] = tileBases[type];
        }
    }
}
