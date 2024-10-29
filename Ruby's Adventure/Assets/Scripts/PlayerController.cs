using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + 0.1f;
        transform.position = position;
    }
}
