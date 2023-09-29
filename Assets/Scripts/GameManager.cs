using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGamePause;
    [SerializeField] private UnityEvent OnGameResume;
    [SerializeField] public UnityEvent OnGameOver;
    [SerializeField] public UnityEvent<int> OnScoreChange;
    [SerializeField] private Player _player;
    [SerializeField] private HighScoreKeeper highScoreKeeper;

    private bool _isGamePause = false;

    private bool _isGameOver = false;

    private int _score;

    private int _highScore;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange.Invoke(_score);
            if (_score - _lastScoreCheckPoint >= 150)
            {
                _lastScoreCheckPoint += 150;
                RegenHP();
            }
        }
    }

    private int _lastScoreCheckPoint = 0;

    private void RegenHP()
    {
        _player.Health = _player.MaxHealth;

    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Score = 0;

        Time.timeScale = 1f;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (!_isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        _isGamePause = true;

        OnGamePause.Invoke();
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _isGamePause = false;

        OnGameResume.Invoke();
    }

    public void GameOver()
    {
        _isGameOver = true;
        highScoreKeeper.UpdateHighScore(Score);
        OnGameOver.Invoke();
    }
}
