using UnityEngine;
using UnityEngine.Events;

public class HighScoreKeeper : MonoBehaviour
{
    public int HighScore;

    private GameUIHandler gameUIHandler;

    public UnityEvent<int> OnHighScoreChange;

    public HighScoreKeeper Instance;

    public HighScoreSO HighScoreSO;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }
        gameUIHandler = FindObjectOfType<GameUIHandler>();
    }

    private void Start()
    {
        gameUIHandler.UpdateHighscore(HighScoreSO.HighScore);
    }

    public void UpdateHighScore(int score)
    {
        if (score > HighScoreSO.HighScore)
        {
            HighScoreSO.HighScore = score;
            OnHighScoreChange.Invoke(score);
        }
    }
}
