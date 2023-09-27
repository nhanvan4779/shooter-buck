using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    private Transform player;
    private float zBoundMax = 45f;
    private float xBoundMax = 45f;
    private float posY = 8f;

    private float maxDistanceToPlayer = 15f;

    public float spawnInterval = 5f;
    public float minSpawnInterval = 1f;
    private float nextSpawn;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            spawnInterval = spawnInterval * 0.9f;
            if (spawnInterval < minSpawnInterval)
            {
                spawnInterval = minSpawnInterval;
            }

            nextSpawn = Time.time + spawnInterval;

            SpawnEnemy();
        }
    }

    private Vector3 RandomPosition()
    {
        float positiveXBound = player.position.x + maxDistanceToPlayer;
        float negativeXBound = player.position.x - maxDistanceToPlayer;

        float positiveZBound = player.position.z + maxDistanceToPlayer;
        float negativeZBound = player.position.z - maxDistanceToPlayer;

        float posX = Random.Range(negativeXBound, positiveXBound);
        posX = Mathf.Clamp(posX, -xBoundMax, xBoundMax);

        float posZ = Random.Range(negativeZBound, positiveZBound);
        posZ = Mathf.Clamp(posZ, -zBoundMax, zBoundMax);

        Vector3 position = new Vector3(posX, posY, posZ);

        return position;

    }

    private Vector3 RandomPositionOnNavMesh()
    {
        Vector3 pos = RandomPosition();
        NavMeshHit hit;
        NavMesh.SamplePosition(pos, out hit, 20f, NavMesh.AllAreas);
        return hit.position;
    }

    private void SpawnEnemy()
    {
        Enemy enemy = ObjectPooler.ShareInstance.GetObject();
        if (enemy != null)
        {
            enemy.transform.position = RandomPositionOnNavMesh();
            enemy.Init();
            enemy.gameObject.SetActive(true);
        }
    }
}
