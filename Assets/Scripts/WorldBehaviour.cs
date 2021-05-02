using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldBehaviour : RoutineBehaviour
{
    private SpawnBehaviour[] spawns;

    public int SpawnCooldown;
    private int lastSpawn;
    
    protected override int ExecutionPeriod => SpawnCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn")
            .Select(x => x.GetComponent<SpawnBehaviour>()).ToArray();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void ExecuteRoutine()
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
