using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKey : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Animator animator;

    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private float walkSpeedScale = 0.5f;

    private float m_vInput;

    private float m_hInput;

    private void Update()
    {
        m_vInput = Input.GetAxis("Vertical");
        m_hInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }

        // Move the character
        Vector3 moveDirection = transform.forward * m_vInput + transform.right * m_hInput;
        characterController.SimpleMove(moveDirection * runSpeed);

        ControlAnimations();
    }

    private void Walk()
    {
        m_vInput = Mathf.Clamp(m_vInput, -walkSpeedScale, walkSpeedScale);
        m_hInput = Mathf.Clamp(m_hInput, -walkSpeedScale, walkSpeedScale);
    }

    private void ControlAnimations()
    {
        animator.SetFloat(Animator.StringToHash("vSpeed_f"), m_vInput);
        animator.SetFloat(Animator.StringToHash("hSpeed_f"), m_hInput);
    }
}
