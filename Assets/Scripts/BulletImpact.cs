using System.Collections;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private BulletHole bulletHole;

    [SerializeField] private float timeToDestroy = 2.5f;

    private void Start()
    {
        StartCoroutine(AutoDestroyIn(timeToDestroy));
    }

    public void SetBulletHoleParent(Transform parent)
    {
        if (bulletHole != null)
        {
            bulletHole.transform.SetParent(parent);
        }
        else
        {
            Debug.LogWarning("Bullet Hole component reference is null!");
        }
    }

    private IEnumerator AutoDestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
