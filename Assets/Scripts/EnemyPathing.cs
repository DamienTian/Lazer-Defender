using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    // This list stores the path points of the enemy path,
    // It stores the transform position of each path point.
    // Why transform? Because way are looking for the position
    WaveConfig waveConfig;
    List<Transform> enemyPaths;
    int enemyPathsIndex = 0;

	// Use this for initialization
	void Start () {
        // Some syntax looks wired ...
        // Why need to access an element.transform.position from List<Transform> ?
        enemyPaths = waveConfig.GetPath();
        transform.position = enemyPaths[enemyPathsIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMove();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void EnemyMove()
    {
        if (enemyPathsIndex <= enemyPaths.Count - 1)
        {
            var targetPos = enemyPaths[enemyPathsIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (transform.position == targetPos)
            {
                enemyPathsIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
