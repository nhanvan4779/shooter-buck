using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    [SerializeField] private GameObject weaponHolder;

    private GunAmmo m_gunAmmo;

    private void Awake()
    {
        m_gunAmmo = weaponHolder.GetComponentInChildren<GunAmmo>();
    }

    private void OnEnable()
    {
        m_gunAmmo.OnAmmoChanged.AddListener(UpdateAmmo);
    }

    private void OnDisable()
    {
        m_gunAmmo.OnAmmoChanged.RemoveListener(UpdateAmmo);
    }

    private void UpdateAmmo()
    {
        ammoText.text = $"{m_gunAmmo.CurrentAmmo}/{m_gunAmmo.AmmoInStock}";
    }
}
