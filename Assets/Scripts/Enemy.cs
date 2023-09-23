using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float detectRange = 10f;
    [SerializeField] protected float detectAngle = 60f;

    protected GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected bool DetectPlayerInSight()
    {
        if (DistanceToPlayer < detectRange)
        {
            Vector3 pointToPlayer = (_player.transform.position - transform.position).normalized;
            float angleToLook = Vector3.Angle(transform.forward, pointToPlayer);

            if (angleToLook < detectAngle)
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
}
