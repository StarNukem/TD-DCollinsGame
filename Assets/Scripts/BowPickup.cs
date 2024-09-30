using UnityEngine;

public class BowPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddItemToInventory("Bow");
            Destroy(gameObject);
        }
    }
}