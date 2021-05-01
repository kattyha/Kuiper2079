using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBehaviour : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnAsteroid()
    {
        Instantiate(AsteroidPrefab, new Vector3(2, 2, 0), Quaternion.identity);
    }
}
