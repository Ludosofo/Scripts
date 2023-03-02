using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMultipleSpawn : MonoBehaviour
{
    // Objetivo: Spawnear multiples objetos de forma controlada para testeos

    public GameObject spawn;
    public int spawnCount = 0;
    public float waitTime = 0.1f;
    public Vector2 zoneSpawn = new Vector2(10f, 5f);

    void Start() {
        InvokeRepeating("Spawn", 1.0f, waitTime);
    }

    private void Spawn(){
        
        if(spawn != null)
        {
            float x = Random.Range(-zoneSpawn[0] / 2, zoneSpawn[0] / 2);
            float y = Random.Range(-zoneSpawn[1] / 2, zoneSpawn[1] / 2);

            Instantiate(spawn, new Vector3(x, y, 0.0f), Quaternion.identity);
            spawnCount++;
        }
    }
}
