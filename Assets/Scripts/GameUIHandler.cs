using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    [SerializeField] private GameObject weaponHolder;

    [SerializeField] private Slider healthBar;

    private GunAmmo m_gunAmmo;

    private Player m_player;

    private void Awake()
    {
        m_gunAmmo = weaponHolder.GetComponentInChildren<GunAmmo>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnEnable()
    {
        m_gunAmmo.OnAmmoChanged.AddListener(UpdateAmmo);
        m_player.OnHealthChange.AddListener(UpdateHealth);
    }

    private void OnDisable()
    {
        m_gunAmmo.OnAmmoChanged.RemoveListener(UpdateAmmo);
        m_player.OnHealthChange?.RemoveListener(UpdateHealth);
    }

    private void UpdateAmmo()
    {
        ammoText.text = $"{m_gunAmmo.CurrentAmmo}/{m_gunAmmo.AmmoInStock}";
    }

    public void UpdateHealth(int value)
    {
        healthBar.value = (float)value / m_player.MaxHealth;
    }
}
