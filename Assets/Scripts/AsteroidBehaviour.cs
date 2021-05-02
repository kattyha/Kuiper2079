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
        var rig = GetComponent<Rigidbody2D>();
        rig.mass = Radius * dencity;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogFormat("OnCollisionEnter2D {0}", collision.gameObject.tag);
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
    
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
            {
                KillPlayer(col.gameObject);
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
        Destroy(player);
    }

    private void ReceiveDamage()
    {
        var parts = 2;
        var r = Radius / parts;
        if (r > 0.25f)
        {
            var clone = Instantiate(Prefab, transform.position, Quaternion.identity);
            var script = clone.GetComponent<AsteroidBehaviour>();
            script.Radius = r;
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
