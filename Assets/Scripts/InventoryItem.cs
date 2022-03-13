using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public enum ItemId { none, cellKey, bottle, bottleOfWater, skull }

    [SerializeField] public ItemId itemId;
    [SerializeField] public string inventoryName;
    [SerializeField] public Sprite icon;
}
