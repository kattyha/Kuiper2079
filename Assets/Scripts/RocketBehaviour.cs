using System;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private readonly int maxSpeed = 5;
    private readonly float enginePower = 1;

    private Rigidbody2D rig { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.centerOfMass = new Vector2(0, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        var thrust = Math.Max(Input.GetAxis("Vertical"), 0) * enginePower;
        var possibleSpeedUp = (maxSpeed - rig.velocity.magnitude) * thrust;
        rig.AddRelativeForce(new Vector2(0, Math.Max(possibleSpeedUp, 0) * rig.mass));
        rig.AddTorque(-1 * Input.GetAxis("Horizontal"));
    }
}
