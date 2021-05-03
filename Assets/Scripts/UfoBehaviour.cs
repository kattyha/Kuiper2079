using UnityEngine;

public class UfoBehaviour : EnemyBehaviour
{
    public float VisionRange;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        var los = playerGameObject.transform.position - transform.position;
        if (los.magnitude <= VisionRange)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, los);
        } 
    }
}
