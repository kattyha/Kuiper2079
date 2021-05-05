using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FitScreenBehaviour : MonoBehaviour
{
    public GameObject FitGameObject;
        
    private Camera camera;
    
    private Renderer fitRenderer;
    
    void Start() 
    {
        camera = GetComponent<Camera>();
        fitRenderer = FitGameObject != null ? FitGameObject.GetComponent<Renderer>() : null;
    }

    void Update() 
    {
        if (!fitRenderer)
        {
            return;
        }
        
        var unitsPerPixel = fitRenderer.bounds.size.x / Screen.width;
        var desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        camera.orthographicSize = desiredHalfHeight;
    }
}
