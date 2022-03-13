using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public List<InventoryItem> inventory;
    [HideInInspector] private InteractableObject target;
    [HideInInspector] private LogText logText;
    [HideInInspector] public bool isShown;

    [Header("Visualization")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float shownPositionY = 145f;
    [SerializeField] private float hiddenPositionY = -145f;
    [SerializeField] private float slideSpeed = 10f;

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
        logText.RemoveText();
    }

    public void ApplyItemOnTarget(InventoryItem item)
    {
        if (target != null)
        {
            Close();
            target.Interact(item);
        }
        else
        {
            return; //TODO
        }
    }
}
