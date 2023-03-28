using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    [SerializeField] public Camera cam;
    [SerializeField] public AudioSource footStep;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float smoothInputSpeed = 0.12f;
    [HideInInspector] public Vector3 direction;
    private float smoothInputVelocity;
    private Vector3 playerVelocity;
    private CharacterController controller;
    private PlayerAnimator playerAnimator;
    
    void Start() {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ProcessMove(Vector2 input) {
        // Takes Vector2 input from the PlayerInputs script
        direction = new Vector3(input.x, 0f, input.y).normalized; // Normalize the values and turn the Vector2 into Vector 3
        playerAnimator.WalkAnim(direction.magnitude); // Play the walk animation based on the direction magnitude

        if (direction.magnitude > 0 && controller.isGrounded) { // If player is moving and not jumping
            footStep.enabled = true; // Enable footStep sound
        }
        else {
            footStep.enabled = false; // Otherwise disable it
        }

        // Gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // If player is on ground reset the vertical velocity
        if (controller.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        // If player is moving process the and apply the movement based on input and camera angle
        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothInputVelocity, smoothInputSpeed);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            controller.Move(playerVelocity * Time.fixedDeltaTime);
        }
    }

    public void Jump() {
        if (controller.isGrounded) {
            footStep.enabled = false;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            playerAnimator.JumpAnim();
        }
    }
}