using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKey : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Animator animator;

    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private float walkSpeedScale = 0.4f;

    private float m_vInput;

    private float m_hInput;

    private void Update()
    {
        m_vInput = Input.GetAxis("Vertical");
        m_hInput = Input.GetAxis("Horizontal");

        ControlAnimations();

        // Hold left shift key for walking instead of running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }

        // Move the character
        Vector3 moveDirection = transform.forward * m_vInput + transform.right * m_hInput;
        characterController.SimpleMove(moveDirection * runSpeed);
    }

    private void Walk()
    {
        m_vInput = Mathf.Clamp(m_vInput, -walkSpeedScale, walkSpeedScale);
        m_hInput = Mathf.Clamp(m_hInput, -walkSpeedScale, walkSpeedScale);
    }

    private void ControlAnimations()
    {
        animator.SetBool(Animator.StringToHash("isWalking_b"), IsWalking);
        animator.SetFloat(Animator.StringToHash("vSpeedNormalized_f"), m_vInput);
        animator.SetFloat(Animator.StringToHash("hSpeedNormalized_f"), m_hInput);
    }

    public bool IsWalking
    {
        get
        {
            if (Mathf.Abs(m_vInput) + Mathf.Abs(m_hInput) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
