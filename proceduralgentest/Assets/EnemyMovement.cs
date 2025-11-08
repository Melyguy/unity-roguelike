using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Transform target;
    public Transform Enemy;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            target = playerObj.transform;
    }

    void Update()
    {
        if (target != null && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
        Enemy.LookAt(target);
    }
}
