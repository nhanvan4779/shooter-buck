using System.Collections;
using UnityEngine;

public class EvadeAttack : MonoBehaviour
{
    [SerializeField] private GameObject evadeGroup;

    public void ShowEvadeText()
    {
        StopAllCoroutines();
        StartCoroutine(ShowEvadeTextEffect());
    }

    IEnumerator ShowEvadeTextEffect()
    {
        evadeGroup.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        evadeGroup.SetActive(true);
        yield return new WaitForSeconds(1f);
        evadeGroup.SetActive(false);
    }
}
