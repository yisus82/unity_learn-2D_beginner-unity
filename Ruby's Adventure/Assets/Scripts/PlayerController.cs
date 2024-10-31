using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    [FormerlySerializedAs("MoveAction")] public InputAction moveAction;
    
    private Rigidbody2D rigidbody2d;
    private Vector2 movement;
    
    private void Start()
    {
        moveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();
    }
    
    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + movement * (speed * Time.fixedDeltaTime));
    }
}
