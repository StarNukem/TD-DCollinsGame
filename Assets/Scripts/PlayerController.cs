using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHearts = 3;
    private int currentHearts;
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    public Image[] heartImages;
    public float moveSpeed = 5f;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public GameObject sword;
    public GameObject bow;
    private bool hasBow = false;
    private bool isUsingBow = false;


    private void Start()
    {
        currentHearts = maxHearts;
        bow.SetActive(false);
        UpdateHeartsUI();
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        if (GameManager.Instance.HasItem("Bow"))
        {
            hasBow = true;
        }

        if (hasBow && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SwitchWeapon();
        }
    }
    void SwitchWeapon()
    {
        isUsingBow = !isUsingBow;

        if (isUsingBow)
        {
            bow.SetActive(true);
            sword.SetActive(false);
        }
        else
        {
            bow.SetActive(false);
            sword.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHearts -= damage;
            if (currentHearts <= 0)
            {
                currentHearts = 0;
                UpdateHeartsUI();
                Destroy(this);
                GameManager.Instance.EndGame();
            }
            else
            {
                StartCoroutine(InvincibilityCoroutine());
                UpdateHeartsUI();
            }
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned! Cannot proceed with invincibility.");
            yield break;
        }

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

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHearts)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }

    public void AddHealth(int healthAmount)
    {
        GameManager.Instance.AddScore(5);
        currentHearts = Mathf.Min(currentHearts + healthAmount, maxHearts);
        UpdateHeartsUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPickup"))
        {
            AddHealth(1);
            Destroy(other.gameObject);
        }
    }
}