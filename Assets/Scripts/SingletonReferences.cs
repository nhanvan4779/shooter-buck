using UnityEngine;

public class SingletonReferences : MonoBehaviour
{
    public static SingletonReferences Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    [SerializeField] private GameObject weaponHolder;

    public GameObject WeaponHolder => weaponHolder;
}
