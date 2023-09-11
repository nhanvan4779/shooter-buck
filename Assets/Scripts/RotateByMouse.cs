using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMouse : MonoBehaviour
{
    [SerializeField] private float angleOverDistance = 2.5f;

    [SerializeField] private Transform cameraHolder;

    [SerializeField] private float maxPitchAngle = 75f;

    [SerializeField] private float minPitchAngle = -75f;

    private float m_pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_pitch = cameraHolder.localEulerAngles.x;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate by mouse horizontally
        float deltaYaw = mouseX * angleOverDistance;
        transform.Rotate(0, deltaYaw, 0);

        // Rotate camera by mouse vertically with angular constraint
        float deltaPitch = -mouseY * angleOverDistance;
        m_pitch = Mathf.Clamp(m_pitch + deltaPitch, minPitchAngle, maxPitchAngle);
        cameraHolder.localEulerAngles = new Vector3(m_pitch, 0, 0);
    }
}
