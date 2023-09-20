using UnityEngine;

public class GunSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip shootingSound;

    [SerializeField] private AudioClip outOfAmmoShootingSound;

    [SerializeField] private AudioClip reloadingSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayShootSFX()
    {
        _audioSource.PlayOneShot(shootingSound);
    }

    public void PlayOutOfAmmoShootSFX()
    {
        _audioSource.PlayOneShot(outOfAmmoShootingSound);
    }

    public void PlayReloadingSFX()
    {
        _audioSource.PlayOneShot(reloadingSound);
    }
}
