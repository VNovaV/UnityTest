using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputTest : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpFoce;
    private DefaultInputActions action;
    private Vector2 moveInput;
    // Start is called before the first frame update
    void Awake()
    {
        action = new DefaultInputActions();
        rb = this.GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        action.Enable();
        action.Player.Jump.performed += OnJump;
    }
    // Update is called once per frame
    private void OnDisable()
    {
        action.Disable();
        action.Player.Jump.performed -= OnJump;
    }
    void Update()
    {
        moveInput = action.Player.Move.ReadValue<Vector2>();
        
    }
    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(Vector3.up * jumpFoce,ForceMode.VelocityChange);
    }

}
