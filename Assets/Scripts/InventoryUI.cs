using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [HideInInspector] public List<InventoryItem> inventoryItems;
    [HideInInspector] public InteractableObject target;
    [HideInInspector] public bool isShown;
    [HideInInspector] private Vector2 targetPosition;
    [HideInInspector] public InventoryItemUI selectedItem;

    [Header("Visualization")]
    [SerializeField] private LogText logText;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float shownPosY = 200f;
    [SerializeField] private float hiddenPosY = -145f;
    [SerializeField] private float slideSpeed = 10f;

    private void Start()
    {
        targetPosition = new Vector2(transform.position.x, hiddenPosY);
    }

    private void FixedUpdate()
    {
        HandlePosition();
    }

    private void HandlePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, slideSpeed * Time.deltaTime);
    }

    public void AddItem(InventoryItem newItem)
    {
        inventoryItems.Add(newItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        inventoryItems.Remove(item);
    }

    public void DrawInventory(InteractableObject newTarget)
    {
        isShown = true;
        targetPosition = new Vector2(transform.position.x, shownPosY);
        Cursor.lockState = CursorLockMode.None;

        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        foreach (InventoryItem itemData in inventoryItems)
        {
            GameObject item = Instantiate(itemPrefab);
            item.transform.SetParent(transform);

            InventoryItemUI itemUI = item.GetComponent<InventoryItemUI>();
            itemUI.inventoryItem = itemData;
            itemUI.inventory = this;
            itemUI.logText = logText;
        }

        target = newTarget;
    }

    public void Close()
    {
        isShown = false;
        targetPosition = new Vector2(transform.position.x, hiddenPosY);

        logText.RemoveText();
        selectedItem = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ApplyItemOnTarget(InventoryItem item)
    {
        Close();
        target.Interact(item);
    }
}
