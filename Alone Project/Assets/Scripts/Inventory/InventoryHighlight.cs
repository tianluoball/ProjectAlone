using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void Show(bool showed)
    {
        highlighter.gameObject.SetActive(showed);
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.width * ItemGrid.tileSizeWidth;
        size.y = targetItem.height * ItemGrid.tileSizeHeight;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        Vector2 position = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

        highlighter.localPosition = position;
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if(targetGrid == null) { return; }
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
        highlighter.transform.SetSiblingIndex(0);
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int positionX, int positionY)
    {
        Vector2 position = targetGrid.CalculatePositionOnGrid(targetItem, positionX, positionY);

        highlighter.localPosition = position;
    }
}
