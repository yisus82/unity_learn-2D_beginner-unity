using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();

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
}
