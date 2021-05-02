using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class AsteroidBehaviour : MonoBehaviour
{
    private float dispersion = 0.5f;
    public float Radius;

    public float dencity = 5;

    private readonly int points = 7;

    public GameObject Prefab;

    private Rigidbody2D rig;
    
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
        rig = GetComponent<Rigidbody2D>();
        rig.mass = Radius * dencity;

        var asteroids = GameObject.FindGameObjectsWithTag("asteroid");
        foreach (var asteroid in asteroids)
        {
            Physics2D.IgnoreCollision(asteroid.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
            {
                KillPlayer(collision.gameObject);
                break;
            }
            case "bullet":
            {
                ReceiveDamage();
                break;
            }
        }
    }

    private void KillPlayer(GameObject player)
    {
        //Destroy(player);
    }

    private void ReceiveDamage()
    {
        var parts = 2;
        var r = Radius / parts;
        if (r > 0.25f)
        {
            var equalAngle = 360 / parts;
            for (var i = 1; i <= parts; i++)
            {
                var fragment = Instantiate(Prefab, transform.position, transform.rotation);
                fragment.transform.rotation *= Quaternion.AngleAxis(equalAngle * i, Vector3.forward);
                var script = fragment.GetComponent<AsteroidBehaviour>();
                script.Radius = r;
                var fragmentRig = fragment.GetComponent<Rigidbody2D>();
                fragmentRig.AddForce((Vector2)fragment.transform.up * 50 + rig.velocity.normalized );
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
