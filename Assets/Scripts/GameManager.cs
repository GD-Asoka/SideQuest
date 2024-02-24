using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Castle castle;
    public Path[] paths;
    public float[] distances;
    public Dictionary<Path,float > pathDistances = new Dictionary<Path,float >();
    public Transform spawnPoint;
    public float shortestDistance = Mathf.Infinity;
    public Path shortestPath;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        paths = FindObjectsByType<Path>(FindObjectsSortMode.None);
        castle = FindFirstObjectByType<Castle>();
    }

    private void Start()
    {
        foreach(Path p in paths)
        {
            float tempDistance = p.CalculateDistanceToCastle(spawnPoint.position);
            pathDistances.Add(p,tempDistance);
        }
        distances = new float[pathDistances.Count];
        for(int i = 0; i < pathDistances.Count; i++)
        {
            distances[i] = pathDistances[paths[i]];
        }

        ShortestDistanceFromSpawn();
    }

    private void ShortestDistanceFromSpawn()
    {
        for(int i = 0; i < distances.Length; i++)
        {
            if(distances[i] < shortestDistance)
            {
                shortestDistance = distances[i];
                shortestPath = paths[i];
            }
        }
    }
}
