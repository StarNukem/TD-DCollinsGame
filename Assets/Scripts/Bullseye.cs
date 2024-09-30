using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullseye : MonoBehaviour
{
    public GameObject hiddenBridge;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Arrow"))
        {
            hiddenBridge.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}