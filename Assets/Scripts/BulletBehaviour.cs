using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;

    public float Lifetime;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    public void Shoot(Vector3 shooterVelocity)
    {
        var rig = GetComponent<Rigidbody2D>();
        rig.velocity = shooterVelocity;
        rig.AddRelativeForce(Vector2.up * Speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
