using UnityEngine;

public class SpaceObjectBehaviour : MonoBehaviour
{
    private Rigidbody2D rig { get; set; }
    
    private Transform world { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        world = GameObject.Find("World").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var position = rig.centerOfMass + rig.position;
        
        var bottomLeft = world.position - world.localScale / 2;
        var topRight = world.position + world.localScale / 2;

        if (position.x < bottomLeft.x)
        {
            transform.position += Vector3.right * world.localScale.x;
        }
        
        if (position.x > topRight.x)
        {
            transform.position -= Vector3.right * world.localScale.x;
        }
        
        if (position.y < bottomLeft.y)
        {
            transform.position += Vector3.up * world.localScale.y;
        }
        
        if (position.y > topRight.y)
        {
            transform.position -= Vector3.up * world.localScale.y;
        }
    }
}
