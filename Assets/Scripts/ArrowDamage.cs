using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Camera mainCamera;
    public float lifetime = 5f;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        if (!IsInViewport())
        {
            Destroy(gameObject);
        }
    }
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
            if (collision.CompareTag("Bullseye"))
        {
            Destroy(gameObject);
        }

        }
    }

    private bool IsInViewport()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        return (viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
                viewportPosition.z > 0);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
