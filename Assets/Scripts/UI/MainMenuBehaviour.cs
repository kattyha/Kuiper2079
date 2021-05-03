using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        if (PlayerStats.Score > 0)
        {
            gameOverPanel.SetActive(true);
            scoreText.text = PlayerStats.Score.ToString();
        }
    }
}
