using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    public InventoryItem inventoryItem;

    private void Start()
    {
        GetComponent<Image>().sprite = inventoryItem.icon;
    }

    public void ChooseItem()
    {
        transform.parent.GetComponent<Inventory>().ApplyItemOnTarget(inventoryItem);
    }
}
