using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] private int maxAmmo;

    public UnityEvent OnAmmoChanged;

    private int _currentAmmo;

    private void Start()
    {
        _currentAmmo = maxAmmo;
    }

    public int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = value;

            if (_currentAmmo >= 0)
            {
                OnAmmoChanged.Invoke();
            }
            else
            {
                _currentAmmo = 0;
            }
        }
    }

    public int MaxAmmo => maxAmmo;
}
