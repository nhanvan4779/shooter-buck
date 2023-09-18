using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private float effectDuration = 0.15f;

    private void Start()
    {
        HideMuzzleFlash();
    }

    public void ShowMuzzleFlash()
    {
        float angle = Random.Range(0, 360f);
        muzzleFlash.transform.localEulerAngles = new Vector3(0f, 0f, angle);
        muzzleFlash.SetActive(true);

        CancelInvoke();
        Invoke(nameof(HideMuzzleFlash), effectDuration);
    }

    private void HideMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
}
