using UnityEngine;

public class BackgroundSong : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip menuSong;

    [SerializeField] private AudioClip backgroundSong;

    [SerializeField] private AudioClip gameOverSong;

    public void PlayMenuBG()
    {
        audioSource.clip = menuSong;
        audioSource.Play();
    }

    public void PlayBG()
    {
        audioSource.clip = backgroundSong;
        audioSource.Play();
    }

    public void PlayGameOver()
    {
        audioSource.clip = gameOverSong;
        audioSource.Play();
    }
}
