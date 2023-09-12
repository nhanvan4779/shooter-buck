using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Coroutine disableShootingState;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

            EnableShootingStateFor(1);
        }
    }

    private void Shoot()
    {
        animator.SetTrigger(Animator.StringToHash("rifleShoot_t"));
        Debug.Log("Shoot the rifle!");
    }

    private IEnumerator DisableShootingStateIn(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(Animator.StringToHash("isShooting_b"), false);
    }

    private void EnableShootingStateFor(float time)
    {
        if (disableShootingState != null)
        {
            StopCoroutine(disableShootingState);
        }
        animator.SetBool(Animator.StringToHash("isShooting_b"), true);
        disableShootingState = StartCoroutine(DisableShootingStateIn(time));
    }
}
