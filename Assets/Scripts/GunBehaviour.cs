using Helpers;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject BulletPrefab;
    
    public float ReloadTime;
    private float? lastShoot;

    private Collider2D parentColider;
    private Transform space;
    
    void Start()
    {
        parentColider = gameObject.GetComponentInParent<Collider2D>();
        space = SpaceHelper.GetSpace().transform;
    }
    
    public void Shoot(Vector3 shooterVelocity)
    {
        if (!lastShoot.HasValue || lastShoot.Value + ReloadTime < Time.time)
        {
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation, space);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), parentColider);
            bullet.GetComponent<BulletBehaviour>().Shoot(shooterVelocity);
            lastShoot = Time.time;
        }
    }
}
