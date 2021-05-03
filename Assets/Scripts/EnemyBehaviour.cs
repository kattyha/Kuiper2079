using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
                ReceiveDamage();
                break;
            }
        }
    }

    private void KillPlayer(GameObject player)
    {
        player.GetComponent<RocketBehaviour>().SufferDamage();
    }

    private void ReceiveDamage()
    {
        Destroy(gameObject);
    }
}
