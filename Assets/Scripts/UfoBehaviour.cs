using UnityEngine;

public class UfoBehaviour : EnemyBehaviour
{
    public float VisionRange;

    public float EngineForce;

    private float maxSpead = 5;
    
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!playerGameObject)
        {
            return;
        }
        
        var los = playerGameObject.transform.position - transform.position;
        if (los.magnitude <= VisionRange)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, los);
            if (rig.velocity.magnitude <= maxSpead)
            {
                rig.AddForce(los.normalized * EngineForce);
            }
        }
        else
        {
            rig.AddForce(-los.normalized * EngineForce);
        }
    }
}
