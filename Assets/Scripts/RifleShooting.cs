using System.Collections;
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

    [SerializeField] private float hitForce = 10f;

    [SerializeField] private UnityEvent OnShoot;

    private GunAmmo m_gunAmmo;

    private GunSoundEffects m_gunSoundEffects;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);

    private float m_nextShootTime;

    private Coroutine disableCombatState;

    private WaitForSeconds combatStateDuration = new WaitForSeconds(1f);

    private bool m_canReload = true;

    private bool m_canShoot = true;

    private void Start()
    {
        bulletTrail.enabled = false;

        m_gunAmmo = weaponHolder.GetComponentInChildren<GunAmmo>();

        m_gunSoundEffects = weaponHolder.GetComponentInChildren<GunSoundEffects>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > m_nextShootTime && m_canShoot)
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
        if (m_gunAmmo.CurrentAmmo > 0)
        {
            Vector3 rayOrigin = aimingCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            bulletTrail.SetPosition(0, gunBarrel.position);
            if (Physics.Raycast(rayOrigin, aimingCamera.transform.forward, out hit, shootingRange, shootableLayer))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                HitSurface hitSurface = hit.collider.GetComponentInChildren<HitSurface>();

                if (enemy != null)
                {

                }

                if (hitSurface != null)
                {
                    hitSurface.PlayBulletImpactSound(hit.point);

                    Quaternion bulletImpactRotation = Quaternion.LookRotation(hit.normal);

                    // Spawn bullet impact visual effect and get Bullet Impact component
                    BulletImpact bulletImpact = Instantiate(hitSurface.BulletImpactPrefab, hit.point, bulletImpactRotation).GetComponent<BulletImpact>();

                    // Set bullet impact's parent to the hit game object to make them disappear together
                    bulletImpact.SetBulletHoleParent(hit.transform);
                }
                else
                {
                    Debug.LogWarning("Hit Surface component is required for hittable surfaces!");
                }

                bulletTrail.SetPosition(1, hit.point);
            }
            else
            {
                bulletTrail.SetPosition(1, rayOrigin + aimingCamera.transform.forward * shootingRange);
            }

            m_gunAmmo.CurrentAmmo--;
            StartCoroutine(ShotEffect());
        }
        else
        {
            Debug.Log("Out of ammo!");
            OutOfAmmoShotEffect();
        }
    }

    // This method is called by reloading animation events
    private void Reload()
    {
        m_gunAmmo.Reload();

        EnableShooting();
    }

    // This method is called by reloading animation events
    private void PlayReloadingSFX()
    {
        m_gunSoundEffects.PlayReloadingSFX();
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

        m_gunSoundEffects.PlayOutOfAmmoShootSFX();

        // Disable shooting for a short amount of time when out of ammo to prevent spamming
        m_canShoot = false;
        CancelInvoke();
        Invoke(nameof(EnableShooting), 0.5f);
    }

    private void EnableShooting()
    {
        m_canShoot = true;
    }
}
