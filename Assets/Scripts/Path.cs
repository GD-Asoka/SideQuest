using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Waypoint[] waypoints;
    public float DistanceToTravel;

    private void Awake()
    {
        Waypoint[] temp = GetComponentsInChildren<Waypoint>();
        waypoints = new Waypoint[temp.Length];
        waypoints = temp;
    }

    private void Start()
    {
        
    }

    public float CalculateDistanceToCastle(Vector3 villagerPosition)
    {
        if (waypoints.Length <= 0)
            return -1;

        DistanceToTravel += Vector3.Distance(villagerPosition, waypoints[0].transform.position);
        for(int i = 0; i < waypoints.Length - 1; i++)
        {
            DistanceToTravel += Vector3.Distance(waypoints[i].transform.position, waypoints[i + 1].transform.position);
        }
        DistanceToTravel += Vector3.Distance(villagerPosition, waypoints[0].transform.position);

        return DistanceToTravel;
    }
}
