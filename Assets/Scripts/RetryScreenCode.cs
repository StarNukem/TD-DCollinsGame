using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScreenCode : MonoBehaviour
{
public void OnRetryButtonClicked()
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.enemiesKilled = 0;
        GameManager.Instance.inventory.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
        GameManager.Instance.startTime = Time.time;

        Debug.Log("Score and other variables reset. Reloading scene: " + SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
