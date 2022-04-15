using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] private Transform player;
    [HideInInspector] private InventoryUI inventoryUI;
    [HideInInspector] private UIController uiController;
    [HideInInspector] private float rotationX;

    [Header("Mouse")]
    [SerializeField] private float mouseSensitivity = 200f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    private void Update()
    {
        if (!inventoryUI.isShown && !uiController.pauseMenuIsShown)
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX - mouseY, -70f, 70f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        player.Rotate(Vector3.up, mouseX);
    }
}
