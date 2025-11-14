using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;

        agent.updatePosition = false;
        agent.updateRotation = true;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null && playerObj.activeSelf)
            target = playerObj.transform;
    }

    void Update()
    {
        if (target != null && agent.isOnNavMesh )
        {
            agent.SetDestination(target.position);
        }
    }

    void FixedUpdate()
    {
        if (agent.isOnNavMesh)
        {
            Vector3 nextPos = agent.nextPosition;
            rb.MovePosition(Vector3.MoveTowards(rb.position, nextPos, agent.speed * Time.fixedDeltaTime));

            agent.nextPosition = rb.position;
        }
    }
}
