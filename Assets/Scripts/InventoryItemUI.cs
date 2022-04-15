using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector] public InventoryItem inventoryItem;
    [HideInInspector] private LogText logText;

    private void Start()
    {
        GetComponent<Image>().sprite = inventoryItem.icon;
        logText = GameObject.FindGameObjectWithTag("Log").GetComponent<LogText>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        logText.SetText(inventoryItem.inventoryName);
    }

    public void ChooseItem()
    {
        transform.parent.GetComponent<InventoryUI>().ApplyItemOnTarget(inventoryItem);
    }
}
