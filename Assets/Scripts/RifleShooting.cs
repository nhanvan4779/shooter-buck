using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float shootInterval = 0.25f;

    [SerializeField] private float shootingRange = 50f;

    [SerializeField] private int rifleDamage = 10;

    [SerializeField] private Camera aimingCamera;

    [SerializeField] private LineRenderer bulletTrail;

    [SerializeField] private Transform gunBarrel;

    [SerializeField] private LayerMask shootableLayer;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);

    private float m_nextShootTime;

    private Coroutine disableShootingState;

    private WaitForSeconds shootingStateDuration = new WaitForSeconds(1f);

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > m_nextShootTime)
        {
            m_nextShootTime = Time.time + shootInterval;

            Shoot();

            EnableShootingStateMomentarily();
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

        StartCoroutine(ShotEffect());
    }

    private IEnumerator DisableShootingState()
    {
        yield return shootingStateDuration;
        animator.SetBool(Animator.StringToHash("isShooting_b"), false);
    }

    private void EnableShootingStateMomentarily()
    {
        if (disableShootingState != null)
        {
            StopCoroutine(disableShootingState);
        }
        animator.SetBool(Animator.StringToHash("isShooting_b"), true);
        disableShootingState = StartCoroutine(DisableShootingState());
    }

    private IEnumerator ShotEffect()
    {
        animator.SetTrigger(Animator.StringToHash("rifleShoot_t"));

        bulletTrail.enabled = true;
        yield return shotDuration;
        bulletTrail.enabled = false;
    }
}
