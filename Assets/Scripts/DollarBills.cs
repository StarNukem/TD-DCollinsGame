using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DollarBills : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public int value = 1;

    private static int counter = 0;

    private void Start()
    {
        UpdateCounterText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            counter += value;
            UpdateCounterText();
            GameManager.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }

    private void UpdateCounterText()
    {
        counterText.text = "Dollars: " + counter.ToString();
    }
}