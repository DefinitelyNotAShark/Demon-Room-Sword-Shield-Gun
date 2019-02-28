using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private float minTimeBetweenSpawn;

    [SerializeField]
    private float maxTimeBetweenSpawn;

    [SerializeField]
    [Tooltip("The number of enemies spawned before the wave stops")]
    private int numberOfEnemiesInWave;

    [SerializeField]
    [Tooltip("The amount of time you get to catch your breath inbetween waves")]
    private float timeBetweenWaves;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform[] spawnPoints;

    private GameObject objectInstance;

	void Start ()
    {
        //StartCoroutine(StartSpawning());//start the spawning loop
	}

    private void Update()
    {
        DebugSpawnOnButtonPress();
    }

    private void DebugSpawnOnButtonPress()
    {
        //If we press any of the buttons we spawn an enemy
        if (OVRInput.GetDown(OVRInput.Button.One) ||
            OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) ||
            OVRInput.GetDown(OVRInput.Button.Four)
            )
        {
            Spawn();
        }
    }

    private IEnumerator StartSpawning()
    {

        for (; ; )//HACK maybe put this in a game loop later?
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int i = 0; i < numberOfEnemiesInWave; i++)
            {
                yield return new WaitForSeconds(ChooseARandomSpawnTime());//waits a random time that it chooses from our min to our max
                Spawn();
            }
        }
    }

    private float ChooseARandomSpawnTime()
    {
        return UnityEngine.Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    private void Spawn()
    {
        objectInstance = Instantiate(enemyPrefab, ChooseARandomSpawnPoint(), transform.localRotation);//spawns our enemy at the position of the spawn point and it's normal rotation 
    }

    /// <summary>
    /// Chooses a point at random from our array of transforms and returns the position
    /// </summary>
    private Vector3 ChooseARandomSpawnPoint()
    {
        int randomPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform t;

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == randomPointIndex)
            {
                t = spawnPoints[i];
                return t.position;
            }
        }
        t = spawnPoints[0];
        return t.position;//if it didn't choose a point, return the first one of the index
    }

}
