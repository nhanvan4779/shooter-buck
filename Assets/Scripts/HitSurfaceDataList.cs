using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitSurfaceData", menuName = "Scriptable Object/Hit Surface Data")]
public class HitSurfaceDataList : ScriptableObject
{
    [SerializeField] private List<HitSurfaceData> _list = new List<HitSurfaceData>();

    public List<HitSurfaceData> List => _list;
}

[Serializable]
public class HitSurfaceData
{
    public SurfaceType SurfaceType => _surfaceType;

    [SerializeField] private SurfaceType _surfaceType;

    public GameObject BulletImpactPrefab => _bulletImpactPrefab;

    [SerializeField] private GameObject _bulletImpactPrefab;

    public AudioClip BulletImpactSound => _bulletImpactSound;

    [SerializeField] private AudioClip _bulletImpactSound;
}
