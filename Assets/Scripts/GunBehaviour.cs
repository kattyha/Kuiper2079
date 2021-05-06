using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject BulletPrefab;
    
    public float ReloadTime;
    private float? lastShoot;

    private Collider2D parentColider;
    
    void Start()
    {
        parentColider = gameObject.GetComponentInParent<Collider2D>();
    }
    
    public void Shoot(Vector3 shooterVelocity)
    {
        if (!lastShoot.HasValue || lastShoot.Value + ReloadTime < Time.time)
        {
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation, transform.parent);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), parentColider);
            bullet.GetComponent<BulletBehaviour>().Shoot(shooterVelocity);
            lastShoot = Time.time;
        }
    }
}
