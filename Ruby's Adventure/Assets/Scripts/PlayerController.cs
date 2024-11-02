using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Launch = Animator.StringToHash("Launch");
    public int health { get; private set; }

    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    
    public InputAction moveAction;
    public InputAction launchAction;
    public InputAction talkAction;
    
    private Rigidbody2D _rigidbody2d;
    private Vector2 _movement;
    private bool _isInvincible;
    private float _invincibleTimer;
    private Animator _animator;
    private Vector2 _moveDirection;
    private Transform _projectiles;

    private void Start()
    {
        moveAction.Enable();
        launchAction.Enable();
        launchAction.performed += LaunchProjectile;
        talkAction.Enable();
        talkAction.performed += Talk;
        health = maxHealth;
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _projectiles = GameObject.FindGameObjectWithTag("Projectiles").transform;
    }
    
    private void Update()
    {
        _movement = moveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(_movement.x, 0) || !Mathf.Approximately(_movement.y, 0))
        {
            _moveDirection = _movement;
            _moveDirection.Normalize();
        }
        
        _animator.SetFloat(MoveX, _moveDirection.x);
        _animator.SetFloat(MoveY, _moveDirection.y);
        _animator.SetFloat(Speed, _movement.magnitude);
        
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
            
            _animator.SetTrigger(Hit);
            _isInvincible = true;
            _invincibleTimer = timeInvincible;
        }
        
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(health / (float)maxHealth);
    }

    private void LaunchProjectile(InputAction.CallbackContext context)
    {
        var projectileObject = Instantiate(projectilePrefab, _rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity, _projectiles);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_moveDirection, 300);
        _animator.SetTrigger(Launch);
    }

    private void Talk(InputAction.CallbackContext context)
    {
        var hit = Physics2D.Raycast(_rigidbody2d.position + Vector2.up * 0.2f, _moveDirection, 1.5f,
            LayerMask.GetMask("NPC"));
        if (hit.collider == null)
        {
            return;
        }

        if (hit.collider.gameObject.CompareTag("NPC"))
        {
            UIHandler.instance.DisplayDialogue();
        }
    }
}
