using System;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class AsteroidBehaviour : MonoBehaviour
{
    private float dispersion = 0.9f;
    public float Radius;

    private readonly int points = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        var controller = GetComponent<SpriteShapeController>();
        controller.spline.Clear();
        for (var i = 0; i < points; i++)
        {
            InsertRandomPoint(controller.spline, i);
        }
        controller.BakeCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InsertRandomPoint(Spline s, int offset)
    {
        var angle = offset * 360 / points;
        var radius = Random.Range((1 - dispersion) * Radius, (1 + dispersion) * Radius);
        var vector = Vector3.up * radius;
        vector = Quaternion.Euler(0, 0, angle) * vector;
        s.InsertPointAt(offset, vector);
    }
}
