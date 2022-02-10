using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public enum InventoryId { cellKey, bottle, bottleOfWater, skull }

    [SerializeField] public InventoryId inventoryId;
    [SerializeField] public Sprite icon;
}
