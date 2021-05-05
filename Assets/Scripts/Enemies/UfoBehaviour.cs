using System;
using UnityEngine;

public class UfoBehaviour : EnemyBehaviour
{
    public float VisionRange;

    public float EnginePower;

    public float MinSpeed;
    public float MaxSpeed;

    protected override void Start()
    {
        base.Start();

        rig.velocity = transform.up * MinSpeed;
    }

    void Update()
    {
        if (!PlayerBehaviour || !PlayerBehaviour.gameObject || PlayerBehaviour.Invisible)
        {
            return;
        }
        
        var los = PlayerBehaviour.gameObject.transform.position - transform.position;
        var targetInRange = los.magnitude <= VisionRange;
        
        if (targetInRange)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, los);
            var possibleSpeedUp = (MaxSpeed - rig.velocity.magnitude) * EnginePower * Time.deltaTime;
            rig.AddRelativeForce(new Vector2(0, Math.Max(possibleSpeedUp, MinSpeed) * rig.mass));
        }
        else
        {
            if (rig.velocity.magnitude <= MinSpeed)
            {
                rig.AddRelativeForce(new Vector2(0, EnginePower * rig.mass));
            }
        }
    }
}
