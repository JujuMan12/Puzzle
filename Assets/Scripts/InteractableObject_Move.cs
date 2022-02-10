using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject_Move : InteractableObject
{
    [SerializeField] private bool shouldSumPosition = true;
    [SerializeField] private bool shouldSumRotation = true;
    [SerializeField] private Vector3 positionChange;
    [SerializeField] private Vector3 rotationChange;

    override public void Interact(InventoryItem usedItem)
    {
        if (!CheckUsedItem(usedItem))
        {
            wrongItemSoundEffect.Play();
            return;
        }

        interactionSoundEffect.Play();

        ChangePositionAndRotation();
        GetComponent<BoxCollider>().enabled = false;

        if (usedItem != null)
        {
            inventoryObject.RemoveItem(usedItem);
            Destroy(usedItem.gameObject);
        }

        StartCoroutine(DestroyObject());
    }

    private void ChangePositionAndRotation()
    {
        if (shouldSumPosition)
        {
            objectToInteract.transform.position += positionChange;
        }
        else
        {
            objectToInteract.transform.position = positionChange;
        }

        if (shouldSumRotation)
        {
            objectToInteract.transform.rotation = Quaternion.Euler(rotationChange) * objectToInteract.transform.rotation;
        }
        else
        {
            objectToInteract.transform.rotation = Quaternion.Euler(rotationChange);
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
