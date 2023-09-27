using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler ShareInstance { get; private set; }

    [SerializeField] private GameObject objectToPool;

    [SerializeField] private int numberToPool;

    private List<Enemy> pooledGameObjects = new List<Enemy>();

    private void Awake()
    {
        ShareInstance = this;
    }

    private void Start()
    {
        for (int i = 0; i < numberToPool; i++)
        {
            Enemy spawn = Instantiate(objectToPool).GetComponent<Enemy>();
            spawn.gameObject.SetActive(false);
            pooledGameObjects.Add(spawn);
            spawn.transform.SetParent(this.transform);
        }
    }

    public Enemy GetObject()
    {
        foreach (var obj in pooledGameObjects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }
}
