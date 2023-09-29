using UnityEngine;

[CreateAssetMenu(fileName = "HighScore", menuName = "Scriptable Object/HighScore")]
public class HighScoreSO : ScriptableObject
{
    [SerializeField] private int _highScore;
    public int HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            if (value > _highScore)
            {
                _highScore = value;
            }
            else
            {
                Debug.Log("Highscore is not registered!");
            }
        }
    }
}
