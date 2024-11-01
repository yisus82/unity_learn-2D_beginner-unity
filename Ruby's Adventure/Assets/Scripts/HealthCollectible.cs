using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healthAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        if (player.health >= player.maxHealth)
        {
            return;
        }
        
        player.ChangeHealth(healthAmount);
        Destroy(gameObject);
    }
}
