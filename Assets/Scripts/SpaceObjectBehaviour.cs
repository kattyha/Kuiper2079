using Helpers;
using UnityEngine;

public class SpaceObjectBehaviour : MonoBehaviour
{
    private Rigidbody2D rig { get; set; }
    private Renderer renderer;
    
    private Transform world { get; set; }

    private Camera mainCamera;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        world = GetComponentInParent<SpaceBehaviour>().World;
        mainCamera = Camera.main;
        renderer = GetComponent<Renderer>();
    }
    
    void Update()
    {
        var isVisibleForMain = mainCamera.IsObjectVisible(renderer);
        if (isVisibleForMain)
        {
            return;
        }

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
