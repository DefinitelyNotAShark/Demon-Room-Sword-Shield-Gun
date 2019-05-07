using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [Header("Wave Details")]
    [SerializeField]
    [Tooltip("Defines waves before the player finishes the game.")]
    private List<Wave> waves;
    [SerializeField]
    [Tooltip("Time between waves.")]
    private float waveDelay = 5f;
    [SerializeField]
    [Tooltip("Maximum number of enemies to allow at once.")]
    private int maxEnemies = 8;

    [Header("Wave Indicators")]
    [SerializeField]
    [Tooltip("Particles to play when starting a new wave")]
    private ParticleSystem waveParticles;

    [Header("Spawn Objects")]
    [SerializeField]
    [Tooltip("Enemy prefabs.")]
    private List<GameObject> enemyPrefabs;
    [SerializeField]
    [Tooltip("Spawn points.")]
    private List<Transform> spawnPoints;

    [HideInInspector]
    public int TotalEnemies {  get; set; }

    [SerializeField]
    [Tooltip("Level animator to let it know when to move to other floors with the wave count.")]
    private Animator elevatorAnimator;

    private int waveNum;//counter for the waves

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(EnemySpawnLoop());
	}

    private IEnumerator EnemySpawnLoop()
    {
        foreach(Wave w in waves)
        {
            waveNum++;//add 1 to the wave counter
            elevatorAnimator.SetInteger("WaveNum", waveNum);//update the animator to match the wave counter

            // Play wave particles and give the player a chance to breathe
            waveParticles.Play();
            yield return new WaitForSeconds(waveDelay);
            // Run through each subwave
            foreach(SubWave sw in w.Subwaves)
            {
                TotalEnemies = 0;
                // Each subwave is divided into a group, which rolls in over time
                foreach(Group g in sw.Groups)
                {
                    for(int i = 0; i < g.Enemies.Count; i++)
                    {
                        while (TotalEnemies >= maxEnemies)
                            yield return null;
                        SpawnEnemy(g, i);
                    }
                    // Wait for a predertermined amount of time before the next group
                    yield return new WaitForSeconds(sw.Delay);
                }
                // Wait until all enemies are gone before spawning the next subwave
                while (TotalEnemies > 0)
                    yield return null;
            }
        }
    }

    private void SpawnEnemy(Group g, int i)
    {
        // Spawn each enemy
        GameObject temp = Instantiate(enemyPrefabs[(int)g.Enemies[i]]);
        // Set each enemy to their given spawn point
        temp.transform.SetPositionAndRotation(spawnPoints[g.SpawnPoints[i]].position, spawnPoints[g.SpawnPoints[i]].rotation);
        // Add to the total number of enemies
        TotalEnemies++;
    }
}

// From here on are a bunch of classes simply to hold data
// Waves hold subwaves hold groups hold enemies
// This allows near-complete customization of the specifics of each wave

/// <summary>
/// Define subwaves to spawn after the previous is defeated
/// </summary>
[System.Serializable]
class Wave
{
    [SerializeField]
    [Tooltip("Subwaves involved in the wave.")]
    List<SubWave> subwaves;

    public List<SubWave> Subwaves { get { return subwaves; } }
}

enum EnemyTypes
{
    Melee
}

/// <summary>
/// Defines groups to spawn with time delay during a wave
/// </summary>
[System.Serializable]
class SubWave
{
    [SerializeField]
    [Tooltip("Groups involved in the subwave.")]
    List<Group> groups;
    [SerializeField]
    [Tooltip("Delay between waves, in seconds.")]
    float groupDelay;

    public List<Group> Groups { get { return groups; } }
    public float Delay { get { return groupDelay; } }
}

/// <summary>
/// Defines a group of enemies and their spawn locations within a subwave
/// </summary>
[System.Serializable]
class Group
{
    [SerializeField]
    [Tooltip("Types of each enemy in the group.")]
    List<EnemyTypes> enemyTypes;
    [SerializeField]
    [Tooltip("Spawn location of each enemy in the group.")]
    List<int> spawnPoints;

    public List<EnemyTypes> Enemies { get { return enemyTypes; } }
    public List<int> SpawnPoints { get { return spawnPoints; } }
}
