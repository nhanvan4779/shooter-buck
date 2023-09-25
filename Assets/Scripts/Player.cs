using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private UnityEvent OnDeath;
    public UnityEvent<int> OnHealthChange;
    [SerializeField] private UnityEvent OnEvade;

    public UnityEvent OnTakeDamage;

    private int _health;

    public int MaxHealth => maxHealth;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            Debug.Log($"Player health is {_health}");
            OnHealthChange.Invoke(_health);
        }
    }

    private void Awake()
    {
        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        OnTakeDamage.Invoke();

        animator.SetTrigger(Animator.StringToHash("getHit_t"));

        if (Health <= 0)
        {
            OnDeath.Invoke();
            Debug.Log("Game over!");
        }
    }

    public void OnEvadeInvoke()
    {
        OnEvade.Invoke();
        Debug.Log("Evade the attack!");
    }
}
