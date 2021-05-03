using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
}
