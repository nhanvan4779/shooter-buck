using UnityEngine;

public class HitSurface : MonoBehaviour
{
    [SerializeField] private SurfaceType surfaceType;

    [SerializeField] private HitSurfaceDataList hitSurfaceData;

    [SerializeField] private AudioSource bulletImpactAudioSource;

    public GameObject BulletImpactPrefab => _bulletImpactPrefab;

    private GameObject _bulletImpactPrefab;

    private AudioClip _bulletImpactSound;

    private void Start()
    {
        // Assign bullet impact prefab and sound matching the surface type according to data in hit surface data scriptable object
        foreach (var data in hitSurfaceData.List)
        {
            if (data.SurfaceType == surfaceType)
            {
                _bulletImpactPrefab = data.BulletImpactPrefab;
                _bulletImpactSound = data.BulletImpactSound;
                return;
            }
        }
    }

    public void PlayBulletImpactSound(Vector3 impactPosition)
    {
        bulletImpactAudioSource.transform.position = impactPosition;
        bulletImpactAudioSource.PlayOneShot(_bulletImpactSound);
    }
}

public enum SurfaceType
{
    Concrete,
    Dirt,
    Metal,
    Sand,
    SoftBody,
    Wood
}
