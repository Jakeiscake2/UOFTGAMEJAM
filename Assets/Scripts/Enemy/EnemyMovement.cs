using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Stolen partially from GMTK with Micheal
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Pathfinding2D pathfinder;
    [SerializeField] private Grid2D grid;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float repathInterval = 0.5f;
    [SerializeField] private float nextWaypointDistance = 0.1f;

    private Rigidbody2D rb;
    private List<Node2D> path;
    private int index;
    private float _repathTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pathfinder.grid = grid;
    }

    public void Tick(Transform target)
    {
        if (target == null)
        {
            Stop();
            return;
        }

        RequestPathTo(target.position);
        FollowPath();
    }

    private void RequestPathTo(Vector2 targetPos)
    {
        _repathTimer -= Time.fixedDeltaTime;
        if (_repathTimer <= 0f)
        {
            _repathTimer = repathInterval;
            pathfinder.FindPath(transform.position, targetPos);
            path = grid.path;
            index = 0;
        }
    }

    void FollowPath()
    {
        if (grid.path == null || index >= grid.path.Count)
        {
            Stop();
            return;
        }
        path = grid.path;
        if (path == null)
        {
            Stop();
            return;
        }

        while (index < path.Count && Vector2.Distance(rb.position, path[index].worldPosition) < nextWaypointDistance)
        {
            index++;
        }


        if (index >= path.Count)
        {
            Stop();
            return;
        }

        Vector2 target = path[index].worldPosition;
        Vector2 dir = (target - rb.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }
}