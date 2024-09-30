using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController playerMovement;

    public int score;
    public int playerDeaths;
    public int enemiesKilled;
    public int totalEnemies;
    public float startTime;
    public float completionTime;
    public GameObject bowIcon;
    public GameObject redKeyIcon;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI timerText;
    public GameObject victoryCanvas;

    public GameObject normalCanvas;
    public GameObject endScreenCanvas;
    public GameObject overlappingCanvas;
    public GameObject titleScreen;
    public GameObject htpCanvas;
    

    public List<string> inventory = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startTime = Time.time;
        UpdateUI();
    }

    void Update()
    {
        completionTime = Time.time - startTime;
        UpdateUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        deathsText = GameObject.Find("Deaths").GetComponent<TextMeshProUGUI>();
        killsText = GameObject.Find("Enemies").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        bowIcon = GameObject.Find("BowIcon");
        redKeyIcon = GameObject.Find("RedKeyIcon");

        normalCanvas = GameObject.Find("UI");
        endScreenCanvas = GameObject.Find("GameOver");
        overlappingCanvas = GameObject.Find("RetryScreen");
        victoryCanvas = GameObject.Find("VictoryScreen");

        normalCanvas.SetActive(true);
        endScreenCanvas.SetActive(false);
        overlappingCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        bowIcon.SetActive(false);
        redKeyIcon.SetActive(false);

        startTime = Time.time;
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void PlayerDied()
    {
        playerDeaths++;
        UpdateUI();
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateUI();
    }

    public void EndGame()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        PlayerDied();
        normalCanvas.SetActive(false);
        endScreenCanvas.SetActive(true);
        StartCoroutine(RetryScreen());
    }

    private IEnumerator RetryScreen()
    {
        yield return new WaitForSeconds(3f);
        overlappingCanvas.SetActive(true);
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (deathsText != null) deathsText.text = "Deaths: " + playerDeaths;
        if (killsText != null) killsText.text = "Kills: " + enemiesKilled + "/" + totalEnemies;
        if (timerText != null) timerText.text = "Time: " + Mathf.Floor(completionTime).ToString() + "s";
    }
    public void ShowVictoryScreen()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
            overlappingCanvas.SetActive(false);
            normalCanvas.SetActive(true);
            endScreenCanvas.SetActive(false);

            scoreText.text = "Score: " + score;
            deathsText.text = "Deaths: " + playerDeaths;
            killsText.text = "Kills: " + enemiesKilled + "/" + totalEnemies;
            timerText.text = "Time: " + Mathf.Floor(completionTime).ToString() + "s";

            Time.timeScale = 0;

            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
        
            Debug.Log("Victory screen displayed.");
            StartCoroutine(GoToEndGame());
        }
        else
        {
            Debug.LogWarning("Victory canvas is not assigned!");
        }
    }
    private IEnumerator GoToEndGame()
    {
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("Loading Credit Scene...");
        SceneManager.LoadScene("Credits");
    }

public void OnStartButtonClicked()
{
    score = 0;
    enemiesKilled = 0;
    inventory.Clear();
    SceneManager.LoadScene("TDGame");
    Time.timeScale = 1;
    startTime = Time.time;
    UpdateUI();

    Debug.Log("Score reset to: " + score);
}
    public void OnHelpButtonClicked()
    {
        htpCanvas.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void OnReturnButtonClicked()
    {
        htpCanvas.SetActive(false);
        titleScreen.SetActive(true);
    }
    public void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void AddItemToInventory(string item)
    {
        if (!inventory.Contains(item))
    {
        inventory.Add(item);

        if (item == "Bow")
        {
            bowIcon.SetActive(true);
        }
        else if (item == "Red Key")
        {
            redKeyIcon.SetActive(true);
        }
    }
    }

    public void RemoveItemFromInventory(string item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
        }
    }

    public bool HasItem(string item)
    {
        return inventory.Contains(item);
    }
}