using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldBehaviour : MonoBehaviour
{
    private SpawnBehaviour[] spawns;

    public int SpawnCooldown;
    private int lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn")
            .Select(x => x.GetComponent<SpawnBehaviour>()).ToArray();
        
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            ActivateSpawn();
            yield return new WaitForSeconds(SpawnCooldown / 1000);
        }
    }
    
    void ActivateSpawn()
    {
        var rnd = lastSpawn;
        while (rnd == lastSpawn)
        {
            rnd = Random.Range(0, spawns.Length);
        }
        var randomSpawn = spawns[rnd];
        randomSpawn.SpawnAsteroid();
        lastSpawn = rnd;
    }
}
