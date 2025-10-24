using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public float chaseRange = 15f;
    public float stopDistance = 2f;
    public float updateRate = 0.5f; // seconds between destination updates

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundCheckDistance = 0.3f;

    private NavMeshAgent agent;
    private float nextUpdateTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Ground check (optional)
        if (!IsGrounded()) return;

        if (distance <= chaseRange)
        {
            if (Time.time >= nextUpdateTime)
            {
                agent.SetDestination(player.position);
                nextUpdateTime = Time.time + updateRate;
            }

        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }

}
