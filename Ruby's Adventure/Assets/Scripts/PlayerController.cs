using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    [FormerlySerializedAs("MoveAction")] public InputAction moveAction;
    
    private void Start()
    {
        moveAction.Enable();
    }
    
    private void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        var position = (Vector2)transform.position + move * (speed * Time.deltaTime);
        transform.position = position;
    }
}
