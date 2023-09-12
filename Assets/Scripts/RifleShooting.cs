using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Coroutine disableShootingState;

    private WaitForSeconds shootingStateDuration = new WaitForSeconds(1f);

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

            EnableShootingStateMomentarily();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger(Animator.StringToHash("rifleShoot_t"));
        Debug.Log("Shoot the rifle!");
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
}
