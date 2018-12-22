using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

	// Use this for initialization
    IEnumerator Start () 
    {
        // do ... while : doing the thing (no stop while loop is true)
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
	}

    // Spawn all the enemies waves
    private IEnumerator SpawnAllWaves()
    {
        // i as wave index
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            // The next wave start when the previous wave's ships show
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    // Generating # of enemies in a path
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {   
        for(int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            /* NEXT line: Initialize the enemy at the startign point of the of path */
            waveConfig.GetPath()[0].transform.position,
            Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }   
    }
}
