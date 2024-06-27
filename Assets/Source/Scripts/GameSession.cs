using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Window _startScreen;
    [SerializeField] private Window _endGameScreen;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private void OnEnable()
    {
        _endGameScreen.ButtonClicked += OnRestartButtonClick;
        _startScreen.ButtonClicked += OnPlayButtonClick;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _endGameScreen.ButtonClicked -= OnRestartButtonClick;
        _startScreen.ButtonClicked -= OnPlayButtonClick;
        _player.Died -= OnPlayerDied;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnPlayerDied()
    {
        Time.timeScale = 0;
        _endGameScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }
    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        _player.Reset();
        _scoreCounter.Reset();
        _enemySpawner.Reset();
        _bulletSpawner.Reset();
    }
}