using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 3;
    public float speed = 1.0f;
    public float directionChangeInterval = 2.0f;
    public int damageToPlayer = 1;

    public float invincibilityDuration = 1.0f;
    public SpriteRenderer spriteRenderer;

    private Vector2 movementDirection;
    private bool isInvincible = false;
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Wander());
    }

    private void Update()
    {
        rb.velocity = movementDirection * speed;
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            movementDirection = Random.insideUnitCircle.normalized;
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Collided with Player");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health -= damage;
        isInvincible = true;
        StartCoroutine(InvincibilityCoroutine());
        GameManager.Instance.AddScore(10);

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        float blinkDuration = invincibilityDuration;
        float blinkInterval = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void Die()
    {
        GameManager.Instance.AddScore(100);
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}