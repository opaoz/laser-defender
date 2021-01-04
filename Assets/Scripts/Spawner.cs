using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    IEnumerator SpawnAllWaves()
    {
        for (var i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        var count = wave.GetNumberOfEnemies();
        var enemy = wave.GetEnemyPrefab();
        var waypoints = wave.GetWaypoints();
        var time = wave.GetTimeBetweenSpawns();


        for (var i = 0; i < count; i++)
        {
            var newEnemy = Instantiate(enemy, waypoints[0].position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(wave);

            yield return new WaitForSeconds(time);
        }
    }
}
