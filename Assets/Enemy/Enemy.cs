using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int health = 5;

    [SerializeField]
    int attackDamage = 1;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float attackDelay = .5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       agent = gameObject.GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //Will just cycle through list until player is in range
        agent.SetDestination(waypoints[0].position);
    }




    public void CheckDie()
    {
        if(health <= 0) 
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator AttackDelay() 
    {
        yield return new WaitForSeconds(attackDelay);
    }
}
