using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Rabbit : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 2.4f;
    [SerializeField] private float chaseRange = 15.5f;
    [SerializeField] private float angleToLookThreshold = 15f;

    private WaitForSeconds disableDuration = new WaitForSeconds(1f);

    private bool _isPlayerInSight;
    private float _nextAttack;
    private bool _isCombatStateEntered = false;

    private bool _getDisable;
    [SerializeField] private bool _isDead;

    private void OnEnable()
    {
        _isDead = false;
        _getDisable = false;
        animator.SetBool(Animator.StringToHash("isDead_b"), false);
    }

    public override void ResetState()
    {
        base.ResetState();
        _isDead = false;
        _getDisable = false;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        _isPlayerInSight = DetectPlayerInSight();

        animator.SetBool(Animator.StringToHash("isPlayerInSight_b"), _isPlayerInSight);

        if (_isPlayerInSight || _isCombatStateEntered)
        {
            Combat();
        }
        else
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (_isDead)
        {
            agent.velocity = Vector3.zero;
            return;
        }
        else if (_getDisable)
        {
            return;
        }
        _speed += agent.acceleration * Time.deltaTime;
        _speed = Mathf.Clamp(_speed, 0, agent.speed);
    }

    float _speed;

    protected override void Combat()
    {
        float _distanceToPlayer = DistanceToPlayer;

        if (_distanceToPlayer > chaseRange)
        {
            _isCombatStateEntered = false;
        }
        else
        {
            _isCombatStateEntered = true;
        }

        if (_distanceToPlayer > attackRange && !_getDisable)
        {
            animator.SetBool(Animator.StringToHash("isInAttackRange_b"), false);

            agent.isStopped = false;

            if (AngleToLookAtPlayer > angleToLookThreshold)
            {
                Vector3 pointToPlayer = (_player.transform.position - transform.position).normalized;

                Quaternion forwardRotation = Quaternion.LookRotation(transform.forward);
                Quaternion pointToPlayerRotation = Quaternion.LookRotation(pointToPlayer);

                transform.rotation = Quaternion.RotateTowards(forwardRotation, pointToPlayerRotation, agent.angularSpeed * Time.deltaTime);
                _speed = agent.speed / 3;

                agent.velocity = pointToPlayer * _speed;
            }

            agent.SetDestination(_player.transform.position);
        }
        else
        {
            animator.SetBool(Animator.StringToHash("isInAttackRange_b"), true);

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

    public override void GetShot(int gunDamage, float hitForce)
    {
        healthBar.gameObject.SetActive(true);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.ResetPath();

        GetPushedBack(hitForce);

        _isCombatStateEntered = true;
        TakeDamage(gunDamage);

    }

    Coroutine pushBackCoroutine;

    private void GetPushedBack(float force)
    {
        Vector3 pushVector = (transform.position - _player.transform.position).normalized;

        agent.velocity = pushVector * force;

        transform.LookAt(_player.transform);
        if (pushBackCoroutine != null)
        {
            StopCoroutine(pushBackCoroutine);
        }

        pushBackCoroutine = StartCoroutine(DisableState());
    }

    IEnumerator DisableState()
    {
        agent.isStopped = true;
        agent.ResetPath();
        _getDisable = true;
        yield return disableDuration;
        _getDisable = false;
        agent.isStopped = false;
    }

    [SerializeField] private UnityEvent OnTakeDamage;

    private void TakeDamage(int damage)
    {
        Health -= damage;

        OnTakeDamage.Invoke();

        if (Health <= 0)
        {
            _isDead = true;
            agent.velocity = Vector3.zero;
            animator.SetBool(Animator.StringToHash("isDead_b"), true);
            Debug.Log("The rabbit gets killed!");
        }
        else
        {
            animator.SetTrigger(Animator.StringToHash("getHit_t"));
        }
    }

    [SerializeField] private UnityEvent OnDeath;

    private void Die()
    {
        OnDeath.Invoke();
    }
}
