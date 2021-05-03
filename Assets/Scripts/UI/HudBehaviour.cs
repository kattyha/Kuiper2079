using UnityEngine;
using UnityEngine.UI;

public class HudBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text playerHealthLabel;
    
    [SerializeField]
    private Text playerBlinkCooldownLabel;

    [SerializeField]
    private Text playerScoreLabel;
    
    private RocketBehaviour player { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RocketBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            return;   
        }
        playerHealthLabel.text = player.Health.ToString();
        var cd = (player.BlinkCooldownFinish - Time.time).GetValueOrDefault(0);
        playerBlinkCooldownLabel.text = cd > 0 ? cd.ToString("N1") : string.Empty;
        playerScoreLabel.text = PlayerStats.Score.ToString();
    }
}
