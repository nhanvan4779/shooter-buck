using UnityEngine;
using UnityEngine.Events;

public class GunAmmo : MonoBehaviour
{
    [SerializeField] private int ammoInStock;

    [SerializeField] private int ammoPerMag;

    public UnityEvent OnAmmoChanged;

    private int _currentAmmo;

    private void Start()
    {
        CurrentAmmo = ammoPerMag;
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

    public int AmmoInStock => ammoInStock;

    public bool IsMagFull => _currentAmmo == ammoPerMag;

    public bool IsOutOfStockAmmo => ammoInStock == 0;

    public void Reload()
    {
        int reloadAmount = ammoPerMag - _currentAmmo;

        if (ammoInStock >= reloadAmount)
        {
            ammoInStock -= reloadAmount;
            CurrentAmmo = ammoPerMag;
        }
        else
        {
            reloadAmount = ammoInStock;
            ammoInStock = 0;
            CurrentAmmo += reloadAmount;
        }
    }
}
