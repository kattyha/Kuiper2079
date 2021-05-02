using System;
using UnityEngine;

public class RocketBehaviour : RoutineBehaviour
{
    private readonly int maxSpeed = 5;
    private readonly float enginePower = 1;
    private readonly float torqueVelocity = -0.2f;
    
    public GameObject BulletPrefab;
    public int ReloadTime;
    public override int ExecutionPeriod => ReloadTime;

    private Rigidbody2D rig { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Vector3.zero;
            return;
        }

        Move();
        base.Update();
    }

    public override void ExecuteRoutine() => Shoot();

    private void Move()
    {
        var thrust = Math.Max(Input.GetAxis("Vertical"), 0) * enginePower;
        var possibleSpeedUp = (maxSpeed - rig.velocity.magnitude) * thrust;
        rig.AddRelativeForce(new Vector2(0, Math.Max(possibleSpeedUp, 0) * rig.mass));
        transform.RotateAround(transform.position,Vector3.forward, torqueVelocity * Input.GetAxis("Horizontal"));
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            Instantiate(BulletPrefab, transform.position, transform.rotation);
        }
    }
}
