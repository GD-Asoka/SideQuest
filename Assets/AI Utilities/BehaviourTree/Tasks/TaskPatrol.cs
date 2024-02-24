using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    private Transform transform;
    private CharacterController controller;
    private NavMeshAgent navMeshAgent;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private float waitTime = 1f, waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        this.transform = transform;
        this.waypoints = waypoints;
        controller = transform.GetComponent<CharacterController>();
    }

    public override NODESTATE Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                waiting = false;
        }
        else
        {
            if (currentWaypointIndex >= waypoints.Length)
            {
                Debug.Log("reached end");
                state = NODESTATE.SUCCESS;
                return state;
            }

            Transform wp = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.1f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                //Keep adding 1 to waypoint index. when current waypoint index = waypoints.length,
                //set it back to zero to avoid index out of range exception as last index = waypoints.length - 1.
                //currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                currentWaypointIndex++;                
            }
            else
            {
                controller.Move((wp.position - transform.position).normalized * VillagerBT.speed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                transform.LookAt(new Vector3(wp.position.x, transform.position.y, wp.position.z));
            }
        }

        Debug.Log("patrolling");
        state = NODESTATE.RUNNING;
        return state;
    }
}
//new Vector3(wp.position.x, transform.position.y, wp.position.z)