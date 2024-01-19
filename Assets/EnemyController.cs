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
    private float health = 10;

    void Start()
    {
        MoveToTarget();
    }

    private void Update()
    {
    }


    void MoveToTarget()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Target");

        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(currentTarget.transform.position);
    }

    private void DestroySelf()
    {
        // Health has reached 0 or below, destroy the enemy
        Destroy(gameObject);
    }

    public void AlterHealth(float damage)
    {
        if (health - damage > 0) { health = 0; }
        else { health -= damage; }

        Debug.Log("Health: " + health);

        if (health == 0) { DestroySelf(); }
    }

}
