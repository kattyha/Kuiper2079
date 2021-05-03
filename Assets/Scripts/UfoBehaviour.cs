using UnityEngine;

public class UfoBehaviour : EnemyBehaviour
{
    public float VisionRange;

    public float EngineForce;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
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
            rig.AddForce(los.normalized * EngineForce);
        } 
    }
}
