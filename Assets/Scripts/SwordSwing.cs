using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public float swordDamage = 1.0f;
    public Collider2D swordCollider;

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            int damageAmount = 1;
            enemy.TakeDamage(damageAmount);
            Debug.Log("Hit enemy for " + damageAmount + " damage");
        }
    }
}
}