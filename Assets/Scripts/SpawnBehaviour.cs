using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBehaviour : MonoBehaviour
{
    private readonly float initialSpeed = 50;
    
    public Vector3 OppositeCorner1;
    public Vector3 OppositeCorner2;

    public GameObject SpawnedPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnAsteroid()
    {
        var asteroid = Instantiate(SpawnedPrefab, transform.position, Quaternion.identity);

        var randomTracerTarget = new Vector2(
            Random.Range(OppositeCorner1.x, OppositeCorner2.x), 
            Random.Range(OppositeCorner1.y, OppositeCorner2.y));

        var tracer = randomTracerTarget - (Vector2)transform.position;
        var rig = asteroid.GetComponent<Rigidbody2D>();
        rig.AddForce(tracer.normalized * initialSpeed * rig.mass);
        rig.AddTorque(Random.Range(5, 30));
    }
}