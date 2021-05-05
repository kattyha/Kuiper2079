using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    private RocketBehaviour player;
    
    [SerializeField]
    private GameObject gameOverPanel;
    
    [SerializeField]
    private Text scoreText;

    public void PlayPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        if (!PlayerStats.Score.HasValue)
        {
            return;
        }
        
        gameOverPanel.SetActive(true);
        scoreText.text = PlayerStats.Score.ToString();
    }
}
