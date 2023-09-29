using System.Collections;
using UnityEngine;

public class AmmoSupply : MonoBehaviour
{
    private GunAmmo gunAmmo;

    [SerializeField] private int amountToRecharge = 30;

    private WaitForSeconds duration = new WaitForSeconds(20f);

    private void Awake()
    {

        gunAmmo = SingletonReferences.Instance.WeaponHolder.GetComponentInChildren<GunAmmo>();

    }

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gunAmmo.AddAmmoToStock(amountToRecharge);
            gunAmmo.Reload();

            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return duration;
        Destroy(gameObject);
    }
}
