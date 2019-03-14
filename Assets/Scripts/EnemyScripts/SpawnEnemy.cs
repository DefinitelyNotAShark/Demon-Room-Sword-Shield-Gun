using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Wave Modifiers")]
    [SerializeField]
    [Tooltip("The total number of waves before the game ends")]
    private int totalWaves = 5;

    [SerializeField]
    [Tooltip("The number of enemies spawned before the wave stops")]
    private int numberOfEnemiesInWave;

    [SerializeField]
    [Tooltip("The amount of time you get to catch your breath inbetween waves")]
    private float timeBetweenWaves;

    [SerializeField]
    [Tooltip("The time between each enemy spawn")]
    private float timeBetweenEnemySpawning;

    [SerializeField]
    [Tooltip("The percentage to increase the enemies. It's multiplied by the enemy number and rounded to whole number. 2 doubles the amount of enemies each round, .5 halfs them...etc")]
    private float enemyIncreasePercentage;

    [Header("Wave Indicators")]
    [SerializeField]
    [Tooltip("Particles to play when starting a new wave")]
    private ParticleSystem waveParticles;

    [Header("Spawn Objects")]
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private bool debugSpawn;

    private GameObject objectInstance;

    [HideInInspector]//has to be public so each enemy instance can reference it when they die
    public  int EnemiesOnScreen = 0;//keeps track of the number of enemies 

    public static event Action WavesCompleted;

	void Start ()
    {
        if(!debugSpawn)
        StartCoroutine(SpawnLoop());//start the spawning loop
	}

    private void Update()
    {
        if (debugSpawn)//spawn at the press of any button
        {
            //check for any button press of ABXY
            if (OVRInput.GetDown(OVRInput.Button.One) ||
                OVRInput.GetDown(OVRInput.Button.Two) ||
                OVRInput.GetDown(OVRInput.Button.Three) ||
                OVRInput.GetDown(OVRInput.Button.Four))
            {
                Spawn();//calls the instantiate function
            }
        }
    }

    private IEnumerator SpawnLoop()//this is the spawn loop
    {
        int completedWaves = 0;
        bool firstWave = true;
        while(completedWaves < totalWaves) // Repeat until we complete all the necessary waves
        {
            if (EnemiesOnScreen == 0)//if there's nothing left to defeat, we start a new wave
            {
                waveParticles.Play();
                //WAIT. give player a chance to look around and catch their breath
                yield return new WaitForSeconds(timeBetweenWaves);//wait until diving into a new wave

                //DO SPAWN 
                for (int i = 0; i < numberOfEnemiesInWave; i++)//for every enemy that should be spawning this wave, wait the time alotted and then spawn them
                {
                    yield return new WaitForSeconds(timeBetweenEnemySpawning);//waits a random time that it chooses from our min to our max
                    Spawn();
                }

                //INCREASE DIFFICULTY
                float tempEnemyNumber = numberOfEnemiesInWave * enemyIncreasePercentage;//increase the number of enemies next round
                numberOfEnemiesInWave = Convert.ToInt32(tempEnemyNumber);//round that number to a whole number
                if (!firstWave)
                    completedWaves++;
                else
                    firstWave = false;
            }
            yield return new WaitForEndOfFrame();//lil pause so we don't crash the game
        }

        if (WavesCompleted != null)
            WavesCompleted.Invoke();
    }

    private void Spawn()
    {
        objectInstance = Instantiate(enemyPrefab, ChooseARandomSpawnPoint(), transform.localRotation);//spawns our enemy at the position of the spawn point and it's normal rotation 
        EnemiesOnScreen++;//add to the enemy on screen counter
    }

        /// <summary>
        /// Chooses a point at random from our array of transforms and returns the position
        /// </summary>
        private Vector3 ChooseARandomSpawnPoint()
        {
            int randomPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Transform t;

            for (int i = 0; i < spawnPoints.Length; i++)
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
