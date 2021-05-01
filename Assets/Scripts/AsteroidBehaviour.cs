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
        Debug.LogFormat("OnTriggerEnter2D {0}", col.gameObject.tag);
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
