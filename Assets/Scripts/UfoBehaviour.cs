using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBehaviour : MonoBehaviour
{
    public float VisionRange;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var los = player.transform.position - transform.position;
        if (los.magnitude <= VisionRange)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, los);
        } 
    }
}
