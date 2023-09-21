using UnityEngine;

public class Rabbit : Enemy
{
    [SerializeField] private Animator animator;

    [SerializeField] private float reachRadius = 0.25f;

    private GameObject _player;

    private bool _isDead;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _isDead = false;
    }

    private void Update()
    {
        ChasePlayer();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            _isDead = true;
            animator.SetTrigger(Animator.StringToHash("getHit_t"));
            animator.SetBool(Animator.StringToHash("isDead_b"), true);
        }
        else
        {
            animator.SetTrigger(Animator.StringToHash("getHit_t"));
        }
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > reachRadius && !_isDead)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
            animator.SetBool("isMoving_b", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isMoving_b", false);
        }
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
