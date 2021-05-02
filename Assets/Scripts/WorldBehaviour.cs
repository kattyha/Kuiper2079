using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldBehaviour : MonoBehaviour
{
    private SpawnBehaviour[] spawns;

    public int SpawnCooldown;
    private DateTime? lastSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn")
            .Select(x => x.GetComponent<SpawnBehaviour>()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        var now = DateTime.Now;
        if (!lastSpawn.HasValue || lastSpawn.Value.AddMilliseconds(SpawnCooldown) < now)
        {
            var randomSpawn = spawns[Random.Range(0, spawns.Length)];
            randomSpawn.SpawnAsteroid();
            lastSpawn = now;
        }
    }
}
