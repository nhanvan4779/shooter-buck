using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMouse : MonoBehaviour
{
    [SerializeField] private float anglePerSecond = 60f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate by mouse horizontally
        float yaw = mouseX * anglePerSecond * Time.deltaTime;
        transform.Rotate(0, yaw, 0);
    }
}
