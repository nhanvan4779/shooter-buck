using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGamePause;
    [SerializeField] private UnityEvent OnGameResume;
    [SerializeField] public UnityEvent OnGameOver;

    private bool _isGamePause = false;

    private bool _isGameOver = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {

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
        OnGameOver.Invoke();
    }
}
