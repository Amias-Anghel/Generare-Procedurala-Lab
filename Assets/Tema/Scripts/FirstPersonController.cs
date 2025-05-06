using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;

    private CharacterController cc;
    private Vector3 velocity;
    private float rotationX = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        // Input
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = moveSpeed * (sprint ? sprintMultiplier : 1f);
        cc.Move(move * speed * Time.deltaTime);

        // Jump + Gravity
        if (cc.isGrounded && velocity.y < 0)
            velocity.y = -2f; // small downward force to keep grounded

        if (cc.isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
