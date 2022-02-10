using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Inventory inventoryObject;
    private PauseMenu pauseMenu;

    [SerializeField] private Transform player;

    [SerializeField] private float mouseSensitivity = 200f;

    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if (!inventoryObject.isShown && !pauseMenu.isShown)
        {
            HandleRotation();
        }
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void HandleRotation()
    {
        if (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX - mouseY, -70f, 70f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        player.Rotate(Vector3.up, mouseX);
    }
}
