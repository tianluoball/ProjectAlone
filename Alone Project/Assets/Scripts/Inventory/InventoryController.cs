using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            highlight.SetParent(value);
        }
    }

    [SerializeField] List<GameItem> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryItem overlapItem;
    public InventoryItem selectedItem;
    RectTransform rectTransform;

    InventoryHighlight highlight;
    InventoryItem itemToHighlight;
    Vector2Int oldPosition;

    private void Awake()
    {
        highlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        if (selectedItem != null)
        {
            selectedItem.GetComponent<RectTransform>().SetParent(selectedItemGrid.GetComponent<RectTransform>());
        }

        ItemDrag();


        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedItemGrid == null)
        {
            highlight.Show(false);
            return;
        }

        HandleHighlight();

        //if (Input.GetMouseButtonDown(1))
        //{
        //    LeftMouseInteract();
        //}
    }

    private void RotateItem()
    {
        if(selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    public void InsertItem(InventoryItem itemToInsert, ItemGrid inventory)
    {
        Vector2Int? positionOnGrid = inventory.FindSpaceForObject(itemToInsert);

        if (positionOnGrid == null)
        {
            return;
        }

        inventory.PlaceItem(itemToInsert, positionOnGrid.Value.x, positionOnGrid.Value.y);
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if(oldPosition == positionOnGrid) { return; }

        oldPosition = positionOnGrid;

        if(selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if(itemToHighlight != null)
            {
                highlight.Show(true);
                highlight.SetSize(itemToHighlight);
                highlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                highlight.Show(false);
            }
        }
        else
        {
            highlight.Show(selectedItemGrid.BoundaryCheck(positionOnGrid.x,positionOnGrid.y,selectedItem.width, selectedItem.height));
            highlight.SetSize(selectedItem);
            highlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }


    public void LeftMouseInteract()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.gameItemData.SlotDimension.Width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.gameItemData.SlotDimension.Height - 1) * ItemGrid.tileSizeHeight / 2;
        }
        Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(position);

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);

        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
