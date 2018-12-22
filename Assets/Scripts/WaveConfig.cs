using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This head line creates a scriptable object in unity
// and you can create a scriptable object directly in unity
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnsRandomEnemies = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return enemyPrefab; }

    public List<Transform> GetPath() 
    {
        // Stores the list of waypoints
        var waveWaypoints = new List<Transform>();

        // Put each point in the path prefab 
        foreach (Transform waypoint in pathPrefab.transform)
        {
            waveWaypoints.Add(waypoint);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnsRandomEnemies() { return spawnsRandomEnemies; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
}
