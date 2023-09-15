using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    [SerializeField] private Camera aimingCamera;

    [SerializeField] private float rayLength = 50f;

    private void Update()
    {
        Vector3 rayOrigin = aimingCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(rayOrigin, aimingCamera.transform.forward * rayLength, Color.green);
    }
}
