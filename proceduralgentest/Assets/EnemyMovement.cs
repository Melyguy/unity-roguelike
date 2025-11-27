using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float stoppingDistance = 1.5f;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // Disable automatic NavMeshAgent movement (we control it manually)
        agent.updatePosition = false;
        agent.updateRotation = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {
        FindNearestPlayer();
    }

    void Update()
    {
        if (target == null)
        {
            FindNearestPlayer();
            return;
        }

        agent.SetDestination(target.position);
        RotateTowardTarget();
    }

    void FixedUpdate()
    {
        if (target == null || !agent.hasPath)
            return;

        // Sync agent before calculating movement
        agent.nextPosition = rb.position;

        Vector3 direction = (agent.steeringTarget - transform.position).normalized;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Sync back after moving
        agent.nextPosition = rb.position;
    }


    private void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            target = null;
            return;
        }

        float minDist = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player.transform;
            }
        }

        target = closest;
    }

    private void RotateTowardTarget()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;

        if (dir.magnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
        }
    }
}
