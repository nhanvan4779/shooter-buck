using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IShootable
{
    [SerializeField] protected int health;

    [SerializeField] protected NavMeshAgent agent;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("The shooting target got shot!");
        }
    }
}
