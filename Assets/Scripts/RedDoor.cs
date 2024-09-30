using System.Collections;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    public GameObject NoKeyCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.HasItem("Red Key"))
        {
            GameManager.Instance.RemoveItemFromInventory("Red Key");
            GameManager.Instance.redKeyIcon.SetActive(false);
            OpenDoor();
        }
        else if (other.CompareTag("Player") && !GameManager.Instance.HasItem("Red Key"))
        {
            StartCoroutine(DontHaveKey());
        }
    }

    private IEnumerator DontHaveKey()
    {
        NoKeyCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        NoKeyCanvas.SetActive(false);
    }

    private void OpenDoor()
    {
        Destroy(gameObject);
    }
}