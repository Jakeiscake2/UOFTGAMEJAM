using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected enum EnemyState { Chasing, Attack }
    protected EnemyMovement movement;
    [SerializeField] protected Transform player;
    [SerializeField] protected LayerMask obstacleMask;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float visionRange;
    protected float attackTimer;

    protected EnemyState state;
    protected void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        player = GameObject.FindWithTag("Player").transform;
    }

    protected void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Chasing: Chasing(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }
    protected virtual void Chasing()
    {

    }
    protected virtual void Attack()
    {

    }

    protected bool CanSee(Transform target)
    {
        if (target == null) return false;

        Vector2 origin = transform.position;
        Vector2 toTarget = (Vector2)target.position - origin;

        if (toTarget.sqrMagnitude > visionRange * visionRange) return false;
        //obsticle inbetween
        if (Physics2D.Raycast(origin, toTarget.normalized, toTarget.magnitude, obstacleMask))
        {
            return false;
        }

        return true;
    }
}
