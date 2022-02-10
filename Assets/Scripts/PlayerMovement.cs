using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private float defaultHeight;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMult = 2f;
    [SerializeField] private float crouchMult = 0.5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask environmentMask;

    private bool isCrouching = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultHeight = transform.localScale.y;
    }

    private void Update()
    {
        HandleMovement();
        HandleVelocity();

        if (Input.GetButtonDown("Crouch") && IsOnGround())
        {
            SwitchCrouching(!isCrouching);
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
