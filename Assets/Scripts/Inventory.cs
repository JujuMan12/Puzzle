using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory;
    private InteractableObject target;
    private LogText logText;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float shownPositionY = 145f;
    [SerializeField] private float hiddenPositionY = -145f;
    [SerializeField] private float slideSpeed = 10f;

    public bool isShown = false;

    private void Start()
    {
        logText = GameObject.FindGameObjectWithTag("Log").GetComponent<LogText>();
    }

    private void Update()
    {
        float posY = transform.position.y;

        if (isShown)
        {
            posY = Mathf.Lerp(posY, shownPositionY, slideSpeed * Time.deltaTime);
        }
        else
        {
            posY = Mathf.Lerp(posY, hiddenPositionY, slideSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }

    public void AddItem(InventoryItem newItem)
    {
        inventory.Add(newItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        inventory.Remove(item);
    }

    public void DrawInventory(InteractableObject newTarget)
    {
        isShown = true;

        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        foreach (InventoryItem itemData in inventory)
        {
            GameObject item = Instantiate(itemPrefab);
            item.transform.SetParent(transform);

            InventoryItemUI itemUI = item.GetComponent<InventoryItemUI>();
            itemUI.inventoryItem = itemData;
        }

        target = newTarget;
    }

    public void Close()
    {
        isShown = false;
        logText.RemoveDelay();
    }

    public void ApplyItemOnTarget(InventoryItem item)
    {
        if (target != null)
        {
            target.Interact(item);
            Close();
        }
        else
        {
            return; //TODO
        }
    }
}
