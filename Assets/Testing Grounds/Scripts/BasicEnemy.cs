using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using static PlayerController;

public class BasicEnemy : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent agent;
    public float lookRadius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(playerController.transform.position, transform.position) <= lookRadius)
        {
            agent.SetDestination(playerController.transform.position);
        }

        if (Vector3.Distance(agent.velocity, new Vector3(0, 0, 0)) > 0)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

public enum EnemyState
{
    Chasing,
    Searching,
    Idle,
}
