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

        // Hold left shift key for moving with walking speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ScaleMovingSpeed(walkSpeedScale);
        }

        ControlAnimations();

        // Move the character
        Vector3 moveDirection = transform.forward * m_vInput + transform.right * m_hInput;
        characterController.SimpleMove(moveDirection * runSpeed);
    }

    private void ScaleMovingSpeed(float speedScale)
    {
        m_vInput = Mathf.Clamp(m_vInput, -speedScale, speedScale);
        m_hInput = Mathf.Clamp(m_hInput, -speedScale, speedScale);
    }

    public bool IsMoving => Mathf.Abs(m_vInput) + Mathf.Abs(m_hInput) > 0;

    private void ControlAnimations()
    {
        animator.SetBool(Animator.StringToHash("isMoving_b"), IsMoving);
        animator.SetFloat(Animator.StringToHash("vSpeedNormalized_f"), m_vInput);
        animator.SetFloat(Animator.StringToHash("hSpeedNormalized_f"), m_hInput);
    }
}
