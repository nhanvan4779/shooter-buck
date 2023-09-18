using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip shootingSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayShootSFX()
    {
        _audioSource.PlayOneShot(shootingSound);
    }
}
