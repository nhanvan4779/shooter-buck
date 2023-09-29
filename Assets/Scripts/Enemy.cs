using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float detectRange = 10f;
    [SerializeField] protected float detectAngle = 60f;
    [SerializeField] protected int attackDamage = 10;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected UnityEvent<int> OnHealthChange;
    [SerializeField] protected Slider healthBar;

    protected Player _player;
    [SerializeField] protected int _health;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            Debug.Log($"Rabbit health is {_health}");
            OnHealthChange.Invoke(_health);
        }
    }

    public void UpdateHealthBar(int health)
    {
        healthBar.value = (float)health / maxHealth;
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Init();
    }

    public void Init()
    {
        Health = maxHealth;
        healthBar.gameObject.SetActive(false);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public virtual void ResetState()
    {
        Health = maxHealth;
    }

    [SerializeField] protected bool _isPlayerDead = false;

    protected bool DetectPlayerInSight()
    {
        if (_isPlayerDead)
        {
            return false;
        }
        if (DistanceToPlayer < detectRange)
        {
            if (AngleToLookAtPlayer < detectAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected float AngleToLookAtPlayer
    {
        get
        {
            Vector3 pointToPlayer = (_player.transform.position - transform.position).normalized;
            return Vector3.Angle(transform.forward, pointToPlayer);
        }
    }

    public float DistanceToPlayer
    {
        get
        {
            return Vector3.Distance(transform.position, _player.transform.position);
        }
    }

    protected virtual void Patrol()
    {

    }

    protected virtual void Combat()
    {

    }

    protected virtual void DealDamage()
    {
        _player.TakeDamage(attackDamage);
    }

    public virtual void GetShot(int gunDamage, float hitForce)
    {

    }

    protected void MissAttack()
    {
        _player.OnEvadeInvoke();
    }

    protected virtual void StopAttackPlayer()
    {
        _isPlayerDead = true;

        Debug.Log("Stop attack!");
    }
}
