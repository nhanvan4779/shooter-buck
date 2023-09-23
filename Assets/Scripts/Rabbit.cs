using UnityEngine;
using UnityEngine.AI;

public class Rabbit : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 2.4f;

    private bool _isPlayerInSight;
    private float _nextAttack;

    private void Update()
    {
        _isPlayerInSight = DetectPlayerInSight();

        animator.SetBool(Animator.StringToHash("isPlayerInSight_b"), _isPlayerInSight);

        if (_isPlayerInSight)
        {
            Combat();
        }
        else
        {
            Patrol();
        }
    }

    protected override void Combat()
    {
        if (DistanceToPlayer > attackRange)
        {
            animator.SetBool(Animator.StringToHash("isInAttackRange_b"), false);

            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
        }
        else
        {
            animator.SetBool(Animator.StringToHash("isInAttackRange_b"), false);

            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            Attack();
        }
    }

    private void Attack()
    {
        transform.LookAt(_player.transform);

        if (Time.time > _nextAttack)
        {
            _nextAttack = Time.time + attackCooldown;
            animator.SetTrigger(Animator.StringToHash("attackTrigger"));
        }
    }
}
