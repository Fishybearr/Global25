using UnityEngine;
using UnityEngine.AI;

public class BadGuyFight : MonoBehaviour
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

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    float detectRadius = 5;

    [SerializeField]
    Transform currentWaypoint;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Animator anim;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        //InvokeRepeating("CheckPlayerInRange", .5f, .25f);

        //Will just cycle through list until player is in range
        //agent.SetDestination(waypoints[0].position);

        currentWaypoint = waypoints[0];

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Magnitude", agent.velocity.magnitude);
        agent.SetDestination(currentWaypoint.position);

        CheckPlayerInRange();

        //need to set some kind of function so that update is not fighting to update multiple agent locations at once.
        //just have a method in update that sets path and change value of path variable

        //set player as attack waypoint and set a timer to stop looking
    }

    //Will be called by invoke repeating
    void CheckPlayerInRange()
    {
        // collider[0] will always be player as it is the only thing on this layer
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        if (cols.Length > 0)
        {
            //means player has been detected
            //set waypoint to player
            //agent.SetDestination(waypoints[1].position);


            //Changes waypoint to player location
            currentWaypoint = playerTransform;
        }
        else
        {
            //switch back to whatever waypoint it was before
            currentWaypoint = waypoints[0];
        }

    }


    public void CheckDie()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
