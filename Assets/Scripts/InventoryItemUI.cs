using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler
{
    [HideInInspector] public InventoryItem inventoryItem;
    [HideInInspector] public InventoryUI inventory;
    [HideInInspector] private LogText logText;

    private void Start()
    {
        GetComponent<Image>().sprite = inventoryItem.inventoryIcon;
        inventory = transform.parent.GetComponent<InventoryUI>();
        logText = GameObject.FindGameObjectWithTag("Log").GetComponent<LogText>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        logText.SetText(inventoryItem.inventoryName);
    }

    public void ChooseItem()
    {
        if (inventory.target != null)
        {
            inventory.ApplyItemOnTarget(inventoryItem);
        }
        else if (inventory.selectedItem != null)
        {
            if (inventory.selectedItem != this)
            {
                CombineWith(inventory.selectedItem);
            }
            else
            {
                inventory.selectedItem = null;
            }
        }
        else
        {
            inventory.selectedItem = this;
        }
    }

    private void CombineWith(InventoryItemUI otherItem)
    {
        PlayerInteraction player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

        if (inventoryItem.combinableWith == otherItem.inventoryItem.itemId)
        {
            player.correctItemSoundEffect.Play();

            Transform inventoryItemsFolder = GameObject.FindGameObjectWithTag("Inventory Items Folder").transform;
            GameObject newItem = Instantiate(inventoryItem.combinationResult, Vector3.zero, Quaternion.identity, inventoryItemsFolder);
            inventory.AddItem(newItem.GetComponent<InventoryItem>());

            inventory.RemoveItem(inventoryItem);
            inventory.RemoveItem(otherItem.inventoryItem);
            Destroy(inventoryItem.gameObject);
            Destroy(otherItem.inventoryItem.gameObject);

            inventory.Close();
        }
        else
        {
            player.wrongItemSoundEffect.Play();
        }
    }
}
