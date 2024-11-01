using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Fixed = Animator.StringToHash("Fixed");

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    public float speed = 1.0f;
    public Direction initialDirection = Direction.Down;
    public float changeTime = 3.0f;
    public int damageAmount = 1;
  
    private Rigidbody2D _rigidbody2d;
    private float _timer;
    private Direction _direction;
    private bool _vertical = true;
    private Animator _animator;
    private bool _isFixed = false;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _direction = initialDirection;
        _vertical = initialDirection is Direction.Up or Direction.Down;
        _animator = GetComponent<Animator>();
        SetAnimation();
    }
    
    private void SetAnimation()
    {
        _animator.SetFloat(MoveX, _direction switch
        {
            Direction.Right => 1,
            Direction.Left => -1,
            _ => 0
        });
        _animator.SetFloat(MoveY, _direction switch
        {
            Direction.Up => 1,
            Direction.Down => -1,
            _ => 0
        });
    }
    
    private void Update()
    {
        if (_isFixed)
        {
            return;
        }
        
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }
        
        if (_vertical)
        {
            _direction = _direction == Direction.Up ? Direction.Down : Direction.Up;
        }
        else
        {
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
        }
        SetAnimation();
        
        _timer = changeTime;
    }

    private void FixedUpdate()
    {   
        if (_isFixed)
        {
            return;
        }
        
        var position = _rigidbody2d.position;
     
        switch (_direction)
        {
            case Direction.Up:
                position.y += speed * Time.fixedDeltaTime;
                break;
            case Direction.Down:
                position.y -= speed * Time.fixedDeltaTime;
                break;
            case Direction.Left:
                position.x -= speed * Time.fixedDeltaTime;
                break;
            case Direction.Right:
                position.x += speed * Time.fixedDeltaTime;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _rigidbody2d.MovePosition(position);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        if (player.health <= 0)
        {
            return;
        }

        player.ChangeHealth(-damageAmount);
    }
    
    public void Fix()
    {
        _isFixed = true;
        _rigidbody2d.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger(Fixed);
    }
}
