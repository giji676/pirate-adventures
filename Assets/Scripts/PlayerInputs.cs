using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    public InputActions inputActions;
    public InputActions.MovementActions movementActions;
    private PlayerMotor playerMotor;

    void Awake() {
        inputActions = new InputActions();
        movementActions = inputActions.Movement;
    }

    void Start() {
        playerMotor = GetComponent<PlayerMotor>();
        movementActions.Jump.performed += ctx => playerMotor.Jump();
    }

    void OnEnable() {
        movementActions.Enable();
    }

    void OnDisable() {
        movementActions.Disable();
    }
   
    void FixedUpdate() {
        playerMotor.ProcessMove(movementActions.Move.ReadValue<Vector2>());
    }
}