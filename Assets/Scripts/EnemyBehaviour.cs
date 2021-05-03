using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int Score;

    protected GameObject playerGameObject;
    private RocketBehaviour player;
    
    protected Rigidbody2D rig;

    protected virtual void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        
        rig = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
            {
                KillPlayer(collision.gameObject);
                break;
            }
            case "bullet":
            {
                ApplyScore();
                ReceiveDamage();
                break;
            }
        }
    }

    private void KillPlayer(GameObject player)
    {
        player.GetComponent<RocketBehaviour>().SufferDamage();
    }

    protected virtual void ReceiveDamage()
    {
        Destroy(gameObject);
    }

    private void ApplyScore()
    {
        PlayerStats.Score += Score;
    }
}
