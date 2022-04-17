using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public enum ItemId { none, cellKey, bottle, bottleOfWater, skull, sword }

    [Header("Item Info")]
    [SerializeField] public ItemId itemId;
    [SerializeField] public string inventoryName;
    [SerializeField] public Sprite inventoryIcon;

    [Header("Combination Info")]
    [SerializeField] public ItemId combinableWith;
    [SerializeField] public GameObject combinationResult;
}
