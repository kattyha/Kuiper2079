using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text payerLivesLabel;
    
    private RocketBehaviour player { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RocketBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        payerLivesLabel.text = player.Lives.ToString();
    }
}
