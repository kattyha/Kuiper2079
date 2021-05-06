using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceBehaviour : MonoBehaviour
{
    private SpawnBehaviour[] spawns;

    public Transform World; 

    public int SpawnCooldown;
    private int lastSpawn;

    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Respawn")
            .Select(x => x.GetComponent<SpawnBehaviour>()).ToArray();
        
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            ActivateSpawn();
            yield return new WaitForSeconds(SpawnCooldown);
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
