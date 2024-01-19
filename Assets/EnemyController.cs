using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject currentTarget;

    private NavMeshAgent agent;

    [SerializeField, Range(10, 100)]
    private float health;

    void Start()
    {
        MoveToTarget();
    }


    void MoveToTarget()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Target");

        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(currentTarget.transform.position);
    }

    public void AlterHealth(float damage)
    {
        if (health - damage > 0) { health = 0; }
        else { health -= damage; }
    }

}
