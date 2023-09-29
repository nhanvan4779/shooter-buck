#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject startMenuGroup;

    [SerializeField] private GameObject creditsGroup;

    [SerializeField] private GameObject howToPlayGroup;

    private void Awake()
    {
        creditsGroup.SetActive(false);
    }

    public void StartGame(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ShowCredits()
    {
        startMenuGroup.SetActive(false);
        creditsGroup.SetActive(true);
    }

    public void BackToMenu()
    {
        howToPlayGroup.SetActive(false);
        creditsGroup.SetActive(false);
        startMenuGroup.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        startMenuGroup.SetActive(false);
        howToPlayGroup.SetActive(true);
    }
}
