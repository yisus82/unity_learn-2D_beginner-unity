using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > 20.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        _rigidbody2d.AddForce(direction * force);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.collider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }

        Destroy(gameObject);
    }
}
