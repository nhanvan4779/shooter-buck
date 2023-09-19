using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Set the parent game object inactive
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("The shooting target got shot!");
        }
    }
}
