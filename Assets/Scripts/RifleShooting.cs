using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RifleShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float shootInterval = 0.1f;

    [SerializeField] private float shootingRange = 50f;

    [SerializeField] private int rifleDamage = 10;

    [SerializeField] private Camera aimingCamera;

    [SerializeField] private LineRenderer bulletTrail;

    [SerializeField] private Transform gunBarrel;

    [SerializeField] private LayerMask shootableLayer;

    [SerializeField] private GameObject weaponHolder;

    [SerializeField] private UnityEvent OnShoot;

    private GunAmmo m_gunAmmo;

    private GunSoundEffects m_gunSoundEffects;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);

    private float m_nextShootTime;

    private Coroutine disableCombatState;

    private WaitForSeconds combatStateDuration = new WaitForSeconds(1f);

    private bool m_canReload = true;

    private void Start()
    {
        bulletTrail.enabled = false;

        m_gunAmmo = weaponHolder.GetComponentInChildren<GunAmmo>();

        m_gunSoundEffects = weaponHolder.GetComponentInChildren<GunSoundEffects>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > m_nextShootTime)
        {
            m_nextShootTime = Time.time + shootInterval;

            Shoot();

            EnableReloading();

            EnableCombatStateMomentarily();
        }
        else if (Input.GetButtonDown("Reload") && m_canReload)
        {
            if (m_gunAmmo.IsMagFull)
            {
                Debug.Log("The mag is full!");
                return;
            }

            if (m_gunAmmo.IsOutOfStockAmmo)
            {
                Debug.Log("Out of ammo in stock!");
                return;
            }

            animator.SetTrigger(Animator.StringToHash("reload_t"));

            DisableReloading();
        }
    }

    private void Shoot()
    {
        Vector3 rayOrigin = aimingCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        bulletTrail.SetPosition(0, gunBarrel.position);
        if (Physics.Raycast(rayOrigin, aimingCamera.transform.forward, out hit, shootingRange, shootableLayer))
        {
            IShootable shootableObject;

            if (hit.collider.TryGetComponent<IShootable>(out shootableObject))
            {
                shootableObject.TakeDamage(rifleDamage);
            }

            bulletTrail.SetPosition(1, hit.point);
        }
        else
        {
            bulletTrail.SetPosition(1, rayOrigin + aimingCamera.transform.forward * shootingRange);
        }

        if (m_gunAmmo.CurrentAmmo > 0)
        {
            m_gunAmmo.CurrentAmmo--;
            StartCoroutine(ShotEffect());
        }
        else
        {
            Debug.Log("Out of ammo!");
            OutOfAmmoShotEffect();
        }
    }

    // Reload method is called by reload animation events
    private void Reload()
    {
        m_gunAmmo.Reload();
    }

    private void EnableReloading()
    {
        m_canReload = true;
    }

    private void DisableReloading()
    {
        m_canReload = false;
    }

    private IEnumerator DisableCombatState()
    {
        yield return combatStateDuration;
        animator.SetBool(Animator.StringToHash("isCombat_b"), false);
    }

    private void EnableCombatStateMomentarily()
    {
        if (disableCombatState != null)
        {
            StopCoroutine(disableCombatState);
        }
        animator.SetBool(Animator.StringToHash("isCombat_b"), true);
        disableCombatState = StartCoroutine(DisableCombatState());
    }

    private IEnumerator ShotEffect()
    {
        animator.SetTrigger(Animator.StringToHash("rifleShoot_t"));

        m_gunSoundEffects.PlayShootSFX();

        OnShoot.Invoke();

        bulletTrail.enabled = true;
        yield return shotDuration;
        bulletTrail.enabled = false;
    }

    private void OutOfAmmoShotEffect()
    {
        animator.SetTrigger(Animator.StringToHash("rifleShoot_t"));
    }
}
