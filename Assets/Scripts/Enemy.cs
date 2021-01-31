using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    //public Transform target;
    public NavMeshAgent agent;

    [SerializeField] float decisionDelay = 3f;
    [SerializeField] Transform objectToChase;
    [SerializeField] Transform[] waypoints;

    int currentWaypoint = 0;

    enum EnemyStates 
    {
        Patrolling,
        Chasing
    }

    [SerializeField] EnemyStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);
        if(currentState == EnemyStates.Patrolling)
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (target)
        {
            agent.SetDestination(target.position);
        }*/

        if(Vector3.Distance(transform.position, objectToChase.position) > 10f)
        {
            currentState = EnemyStates.Patrolling;
        }
        else
        {
            currentState = EnemyStates.Chasing;
        }

        if(currentState == EnemyStates.Patrolling)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 1.9f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
 
    void SetDestination() {
        if(currentState == EnemyStates.Chasing) 
        agent.SetDestination(objectToChase.position);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
 
    private void OnDrawGizmos()
     {
         Gizmos.DrawWireSphere(transform.position, 5f);
     }
}
