using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{

    public const float tileSizeWidth = 64;
    public const float tileSizeHeight = 64;



    InventoryItem[,] inventoryItemSlot;

    Vector2 positionOntheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    RectTransform rectTransform;

    [SerializeField] int gridSizeWidth = 6;
    [SerializeField] int gridSizeHeight = 6;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null)
        {
            return null;
        }

        CleanGridRef(toReturn);

        return toReturn;

    }

    private void CleanGridRef(InventoryItem item)
    {
        for (int ix = 0; ix < item.width; ix++)
        {
            for (int iy = 0; iy < item.height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);

        rectTransform.sizeDelta = size;
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.height + 1;
        int width = gridSizeWidth - itemToInsert.width + 1;

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvaliableSpace(x, y, itemToInsert.width, itemToInsert.height) == true)
                {
                    return new Vector2Int(x, y);
;               }
            }
        }
        return null;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOntheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOntheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int) (positionOntheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOntheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int positionX, int positionY, ref InventoryItem overlapItem)
    {
        if (BoundaryCheck(positionX, positionY, inventoryItem.width, inventoryItem.height) == false)
        {
            return false;
        }

        if (OverlapCheck(positionX, positionY, inventoryItem.width, inventoryItem.height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridRef(overlapItem);
        }

        PlaceItem(inventoryItem, positionX, positionY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int positionX, int positionY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.width; x++)
        {
            for (int y = 0; y < inventoryItem.height; y++)
            {
                inventoryItemSlot[positionX + x, positionY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = positionX;
        inventoryItem.onGridPositionY = positionY;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, positionX, positionY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int positionX, int positionY)
    {
        Vector2 position = new Vector2();
        position.x = positionX * tileSizeWidth + tileSizeWidth * inventoryItem.width / 2;
        position.y = -(positionY * tileSizeHeight + tileSizeHeight * inventoryItem.height / 2);
        return position;
    }

    private bool OverlapCheck(int positionX, int positionY, int width, int height, ref InventoryItem overlapItem)
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[positionX + x, positionY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[positionX + x, positionY + y];
                    }
                    else
                    {
                        if(overlapItem != inventoryItemSlot[positionX + x, positionY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private bool CheckAvaliableSpace(int positionX, int positionY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[positionX + x, positionY + y] != null)
                {

                      return false;

                }
            }
        }
        return true;
    }

    bool PositionCheck(int positionX, int positionY)
    {
        if(positionX < 0 || positionY < 0)
        {
            return false;
        }

        if(positionX >= gridSizeWidth || positionY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundaryCheck(int positionX, int positionY, int width, int height)
    {
        if (PositionCheck(positionX, positionY) == false)
        {
            return false;
        }

        positionX += width - 1 ;
        positionY += height - 1 ;

        if (PositionCheck(positionX, positionY) == false)
        {
            return false;
        }

        return true;
    }
}
