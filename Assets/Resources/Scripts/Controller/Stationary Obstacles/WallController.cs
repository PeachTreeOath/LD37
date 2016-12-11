using UnityEngine;

internal class WallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("collision_wood");
        }
    }
}