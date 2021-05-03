using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;

    public float Lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
