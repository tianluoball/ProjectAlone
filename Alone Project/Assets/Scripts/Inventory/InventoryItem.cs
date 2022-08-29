using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameItem gameItemData;

    public int height
    {
        get {
            if (rotated == false)
            {
                return gameItemData.SlotDimension.Height;
            }
            return gameItemData.SlotDimension.Width; 
        }
    }

    public int width
    {
        get
        {
            if (rotated == false)
            {
                return gameItemData.SlotDimension.Width;
            }
            return gameItemData.SlotDimension.Height;
        }
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;

    internal void Set(GameItem gameItem)
    {
        this.gameItemData = gameItem;

        Vector2 size = new Vector2();
        size.x = width * ItemGrid.tileSizeWidth;
        size.y = height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0,0,rotated == true? 90f :0f);
    }
}
