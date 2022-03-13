using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_Move : InteractableObject
{
    [HideInInspector] private bool isActivated = false;
    [HideInInspector] private Vector3 targetPosition;
    [HideInInspector] private Quaternion targetRotation;

    [Header("Move Parameters")]
    [SerializeField] private Vector3 positionChange;
    [SerializeField] private Vector3 rotationChange;

    [Header("Move Speed")]
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private float rotationSpeed = 100f;

    override public void Start()
    {
        base.Start();
        targetPosition = objectToInteract.transform.position;
        targetRotation = objectToInteract.transform.rotation;
    }

    private void Update()
    {
        if (isActivated)
        {
            Vector3 currentPosition = objectToInteract.transform.position;
            Quaternion currentRotation = objectToInteract.transform.rotation;

            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime);
            currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

            objectToInteract.transform.position = currentPosition;
            objectToInteract.transform.rotation = currentRotation;
        }
    }

    override public void Interact(InventoryItem usedItem)
    {
        if (!CheckUsedItem(usedItem))
        {
            wrongItemSoundEffect.Play();
            return;
        }

        interactionSoundEffect.Play();

        targetPosition += positionChange;
        targetRotation = Quaternion.Euler(rotationChange) * objectToInteract.transform.rotation;
        isActivated = true;

        if (usedItem != null)
        {
            inventoryObject.RemoveItem(usedItem);
            Destroy(usedItem.gameObject);
        }

        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
