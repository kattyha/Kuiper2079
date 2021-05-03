using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RocketBehaviour : MonoBehaviour
{
    private readonly int maxSpeed = 5;
    private readonly float enginePower = 1;
    private readonly float torqueVelocity = -0.2f;

    public int Health;
    
    public GameObject BulletPrefab;
    
    public float ReloadTime;
    private float? lastShoot;
    
    public float HyperBlinkCooldown;
    private float? lastBlink;

    public float? BlinkCooldownFinish => lastBlink + HyperBlinkCooldown;

    private Rigidbody2D rig { get; set; }
    
    private new Renderer renderer { get; set; }
    
    private new ParticleSystem particleSystem { get; set; }
    
    private new Collider2D collider { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();
        collider = GetComponent<Collider2D>();

        PlayerStats.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
            return;
        }

        Move();
        Shoot();
        HyperBlink();
    }

    public void SufferDamage()
    {
        Health--;
        if (Health <= 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
        StartCoroutine(Blink(3));
    }
    
    IEnumerator Blink(int blinks)
    {
        for (var i = 0; i < blinks; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    private void Move()
    {
        var thrust = Math.Max(Input.GetAxis("Vertical"), 0) * enginePower;
        particleSystem.Emit(thrust > 0 ? 1 : 0);
        var possibleSpeedUp = (maxSpeed - rig.velocity.magnitude) * thrust;
        rig.AddRelativeForce(new Vector2(0, Math.Max(possibleSpeedUp, 0) * rig.mass));
        transform.RotateAround(transform.position,Vector3.forward, torqueVelocity * Input.GetAxis("Horizontal"));
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            var now = Time.time;
            if (!lastShoot.HasValue || lastShoot.Value + ReloadTime < now)
            {
                var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                lastShoot = now;
            }
        }
    }

    private void HyperBlink()
    {
        if (Input.GetButton("Jump"))
        {
            var mainCamera = Camera.main;
            var now = Time.time;
            if (!lastBlink.HasValue || lastBlink.Value + HyperBlinkCooldown < now)
            {
                
                var y = Random.Range(
                    mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).y,
                    mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                var x = Random.Range(
                    mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).x,
                    mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

                var blinkTarget = new Vector2(x, y);
                transform.Translate(blinkTarget);
                lastBlink = now;
            }
        }
    }
}
