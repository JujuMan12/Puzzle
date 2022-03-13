using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [HideInInspector] private string interactTipText;
    [HideInInspector] private InteractableObject target;
    [HideInInspector] private Inventory inventoryObject;
    [HideInInspector] private Image interactIcon;
    [HideInInspector] private Image inventoryIcon;
    [HideInInspector] private Image analyzeIcon;
    [HideInInspector] private bool canInteract;

    [Header("Parameters")]
    [SerializeField] public float interactionRadius = 2.5f;

    private void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        interactIcon = GameObject.FindGameObjectWithTag("Interact Icon").GetComponent<Image>();
        inventoryIcon = GameObject.FindGameObjectWithTag("Inventory Icon").GetComponent<Image>();
        analyzeIcon = GameObject.FindGameObjectWithTag("Analyze Icon").GetComponent<Image>();

        HandleIconsVisibility();
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, interactionRadius) && rayHit.collider.CompareTag("Interactable"))
        {
            target = rayHit.collider.GetComponent<InteractableObject>();
            if (rayHit.distance <= target.interactionRadius && !canInteract)
            {
                canInteract = true;
                HandleIconsVisibility();
            }
        }
        else if (canInteract)
        {
            canInteract = false;
            target = null;
            HandleIconsVisibility();
        }
    }

    private void Update()
    {
        if (canInteract && !inventoryObject.isShown)
        {
            if (Input.GetButtonDown("Interact"))
            {
                target.Interact(null);
            }
            else if (Input.GetButtonDown("Analyze") && target.analyzeText != null)
            {
                target.Analyze();
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            if (!inventoryObject.isShown && target?.requiredItemId != InventoryItem.ItemId.none)
            {
                inventoryObject.DrawInventory(target);
            }
            else
            {
                inventoryObject.Close();
            }
        }
    }

    private void HandleIconsVisibility()
    {
        Color interactIconColor = interactIcon.color;
        Color inventoryIconColor = inventoryIcon.color;
        Color analyzeIconColor = analyzeIcon.color;

        if (canInteract)
        {
            interactIconColor.a = 1f;
            if (target.requiredItemId != InventoryItem.ItemId.none)
            {
                inventoryIconColor.a = 1f;
            }
            else
            {
                inventoryIconColor.a = 0f;
            }

            if (target.analyzeText != "")
            {
                analyzeIconColor.a = 1f;
            }
            else
            {
                analyzeIconColor.a = 0f;
            }
        }
        else
        {
            interactIconColor.a = 0f;
            inventoryIconColor.a = 0f;
            analyzeIconColor.a = 0f;
        }

        interactIcon.color = interactIconColor;
        inventoryIcon.color = inventoryIconColor;
        analyzeIcon.color = analyzeIconColor;
    }
}
