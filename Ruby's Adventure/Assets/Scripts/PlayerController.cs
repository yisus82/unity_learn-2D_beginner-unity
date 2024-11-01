using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public int health { get; private set; }

    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    
    [FormerlySerializedAs("MoveAction")] public InputAction moveAction;
    
    private Rigidbody2D _rigidbody2d;
    private Vector2 _movement;
    private bool _isInvincible;
    private float _invincibleTimer;

    private void Start()
    {
        moveAction.Enable();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    
    private void Update()
    {
        _movement = moveAction.ReadValue<Vector2>();
        if (!_isInvincible)
        {
            return;
        }
        _invincibleTimer -= Time.deltaTime;
        if (_invincibleTimer < 0)
        {
            _isInvincible = false;
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _movement * (speed * Time.fixedDeltaTime));
    }
    
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible)
            {
                return;
            }
            
            _isInvincible = true;
            _invincibleTimer = timeInvincible;
        }
        
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(health / (float)maxHealth);
    }
}
