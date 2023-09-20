using System.Collections;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;

    private MeshRenderer _meshRenderer;

    private void OnDisable()
    {
        // Destroy bullet hole game object when it gets disabled because the parent game object gets disabled (for optimization)
        Destroy(gameObject);
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        // Set bullet hole texture offset to a random value to get a random bullet hole texture out of 4 available ones
        _meshRenderer.material.SetTextureOffset("_MainTex", RandomOffset);

        StartCoroutine(AutoDestroyIn(timeToDestroy));
    }

    private IEnumerator AutoDestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // Generate random offset vector with x and y are either 0 or 0.5
    private Vector2 RandomOffset
    {
        get
        {
            int factor1 = Random.Range(1, 3);
            int factor2 = Random.Range(1, 3);

            float x = 1 - 1f / factor1;
            float y = 1 - 1f / factor2;

            return new Vector2(x, y);
        }
    }
}
