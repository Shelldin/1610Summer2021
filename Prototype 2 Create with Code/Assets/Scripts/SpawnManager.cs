using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    
    
    private Vector3 animalSpawn;
    private float spawnRangeX = 20f;
    private float spawnPosZ = 20f;

    public float startDelay = 2f;
    public float spawnInterval = 1.5f;

    private void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }
    

    private void SpawnRandomAnimal()
    {
        animalSpawn = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], animalSpawn, animalPrefabs[animalIndex].transform.rotation);
    }
}
