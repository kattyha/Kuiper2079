using System;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class AsteroidBehaviour : EnemyBehaviour
{
    private float dispersion = 0.5f;
    public float Radius;
    
    private readonly float initialSpeed = 50;
    
    public float dencity = 5;

    private readonly int points = 7;

    public GameObject Prefab;
    
    protected override void Start()
    {
        base.Start();
        
        var controller = GetComponent<SpriteShapeController>();
        controller.spline.Clear();
        for (var i = 0; i < points; i++)
        {
            InsertRandomPoint(controller.spline, i);
        }
        controller.BakeCollider();
        
        rig.mass = Radius * dencity;

        Score = (int)Math.Round(Score * Radius);

        var asteroids = GameObject.FindGameObjectsWithTag("asteroid");
        foreach (var asteroid in asteroids)
        {
            Physics2D.IgnoreCollision(asteroid.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        rig.AddForce(transform.up * initialSpeed * rig.mass);
        rig.AddTorque(Random.Range(1, 30));
    }

    protected override void ReceiveDamage()
    {
        var parts = 2;
        var r = Radius / parts;
        if (r > 0.25f)
        {
            var equalAngle = 360 / parts + 90;
            for (var i = 1; i <= parts; i++)
            {
                var fragment = Instantiate(Prefab, transform.position, transform.rotation);
                fragment.transform.rotation *= Quaternion.AngleAxis(equalAngle * i, Vector3.forward);
                var fragmentScript = fragment.GetComponent<AsteroidBehaviour>();
                fragmentScript.Radius = r;
            }
        }

        Destroy(gameObject);
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
