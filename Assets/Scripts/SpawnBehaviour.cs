using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class SpawnSettings
{
    [SerializeField]
    public GameObject Prefab;

    [SerializeField]
    public float Probability;
}

public class SpawnBehaviour : MonoBehaviour
{
    public Vector3 OppositeCorner1;
    public Vector3 OppositeCorner2;

    [SerializeField]
    public List<SpawnSettings> Spawned;
    
    public void SpawnAsteroid()
    {
        var placedGameObject = Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity, transform.parent);

        var randomTracerTarget = new Vector2(
            Random.Range(OppositeCorner1.x, OppositeCorner2.x), 
            Random.Range(OppositeCorner1.y, OppositeCorner2.y));

        var tracer = randomTracerTarget - (Vector2)transform.position;
        placedGameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, tracer);
    }

    private GameObject GetRandomPrefab()
    {
        var rnd = Random.Range(0f, 1f);
        var sum = 0f;
        GameObject prefab = null;
        var ordered = Spawned.OrderBy(x => x.Probability);
        foreach (var spawned in ordered)
        {
            sum += spawned.Probability;
            prefab = spawned.Prefab;
            if (rnd <= sum)
            {
                break;
            }
        }
        
        return prefab;
    }
}