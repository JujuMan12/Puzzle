using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] private InventoryUI inventoryUI;
    [HideInInspector] private UIController uiController;
    [HideInInspector] private Vector3 velocity;
    [HideInInspector] private float defaultHeight;
    [HideInInspector] private bool isCrouching;

    [Header("Characteristics")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMult = 2f;
    [SerializeField] private float crouchMult = 0.5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Environment Check")]
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask environmentMask;

    private void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        defaultHeight = transform.localScale.y;
    }

    private void Update()
    {
        if (!inventoryUI.isShown && !uiController.pauseMenuIsShown)
        {
            HandleMovement();
            HandleVelocity();

            if (Input.GetButtonDown("Crouch") && IsOnGround())
            {
                SwitchCrouching(!isCrouching);
            }
        }
    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * dirX + transform.forward * dirY;

        if (IsOnGround())
        {
            if (Input.GetButton("Sprint"))
            {
                direction *= sprintMult;
                SwitchCrouching(false);
            }
            else if (isCrouching)
            {
                direction *= crouchMult;
            }
        }

        controller.Move(direction * speed * Time.deltaTime);
    }

    private void SwitchCrouching(bool newState)
    {
        isCrouching = newState;
        float newHeight = defaultHeight;

        if (isCrouching)
        {
            newHeight /= 2f;
        }

        transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
    }

    private void HandleVelocity()
    {
        if (IsOnGround() || IsUnderCeiling() && velocity.y > -1f)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (IsOnGround() && Input.GetButton("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            SwitchCrouching(false);
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsOnGround()
    {
        return Physics.CheckSphere(groundCheck.position, 0.25f, environmentMask);
    }

    private bool IsUnderCeiling()
    {
        return Physics.CheckSphere(ceilingCheck.position, 0.25f, environmentMask);
    }
}
