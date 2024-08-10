using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI LivesText;
    
    public Button RestartButton;
    public GameObject TitleScreen;
    public GameObject PauseScreen;
    public GameObject Volume;

    public bool IsGameActive;
    private bool _isGamePaused;
    private int _score;
    private int _livesCount;
    private float _spawnRate;
    void Start()
    {
        GameOverText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        LivesText.gameObject.SetActive(false);
        Volume.gameObject.SetActive(true);
        _isGamePaused = false;
        PauseScreen.gameObject.SetActive(_isGamePaused);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) CheckForPause();
    }
    private void CheckForPause()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
        PauseScreen.gameObject.SetActive(_isGamePaused);
    }
    private IEnumerator SpawnTarget()
    {
        while (IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        ScoreText.text = "Score: " + _score;
        if (_score < 0) GameOver();
    }
    public void DecreaseLiveCount()
    {
        if (IsGameActive)
        {
            _livesCount--;
            LivesText.text = "Lives: " + _livesCount;
        }
        if (_livesCount == 0) GameOver();
    }
    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        IsGameActive = false;
        RestartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RestartButton.gameObject.SetActive(false);
    }
    public void StartGame(int difficulty)
    {
        Volume.gameObject.SetActive(false);
        IsGameActive = true;
        _score = 0;

        _livesCount = 3;
        LivesText.gameObject.SetActive(true);
        LivesText.text += _livesCount;
        

        _spawnRate = 1.0f/difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        TitleScreen.SetActive(false);
    }
}
