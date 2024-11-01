using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.0f;
    [FormerlySerializedAs("MoveAction")] public InputAction moveAction;
    
    private Rigidbody2D _rigidbody2d;
    private Vector2 _movement;
    private int _currentHealth;
    
    private void Start()
    {
        moveAction.Enable();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
    }
    
    private void Update()
    {
        _movement = moveAction.ReadValue<Vector2>();
    }
    
    private void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _movement * (speed * Time.fixedDeltaTime));
    }
    
    public void ChangeHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
    }
}
