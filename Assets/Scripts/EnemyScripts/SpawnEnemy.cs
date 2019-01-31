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
    private GameObject enemyPrefab;

    private GameObject objectInstance;

	void Start ()
    {
        StartCoroutine(StartSpawning());//start the spawning loop
	}

    private IEnumerator StartSpawning()
    {
        for(; ; )//HACK maybe put this in a game loop later?
        {
            yield return new WaitForSeconds(ChooseARandomSpawnTime());//waits a random time that it chooses from our min to our max
            Spawn();
        }
    }

    private float ChooseARandomSpawnTime()
    {
        return UnityEngine.Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    private void Spawn()
    {
        objectInstance = Instantiate(enemyPrefab, this.transform, true);//spawns our enemy at the position of the spawn point and it's normal rotation 
    }

}
