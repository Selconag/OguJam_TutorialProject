using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [Space]
    [SerializeField] private UnityEvent onMoveStarted = null;
    [SerializeField] private UnityEvent onMoveStopped = null;

    private Vector2 movementInput = Vector2.zero;

    private PlayerInputs inputs = null;
    private new Rigidbody2D rigidbody = null;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (inputs is null)
        {
            inputs = new();

            inputs.Movement.Move.started += OnMoveStarted;
            inputs.Movement.Move.performed += OnMovePerformed;
            inputs.Movement.Move.canceled += OnMoveCanceled;
        }
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = movementInput.x * moveSpeed;
        rigidbody.velocity = velocity;
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        onMoveStarted?.Invoke();
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
        onMoveStopped?.Invoke();
    }
}
