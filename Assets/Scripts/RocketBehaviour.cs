using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketBehaviour : MonoBehaviour
{
    public float MaxSpeed;
    public float EnginePower;
    private readonly float torqueVelocity = 100f;

    public int Health;
    public bool Invisible { get; private set; }
    
    public float HyperBlinkCooldown;
    private float? lastBlink;

    private GunBehaviour[] Armaments;
    
    public float? BlinkCooldownFinish => lastBlink + HyperBlinkCooldown;

    private Rigidbody2D rig { get; set; }
    
    private new Renderer renderer { get; set; }
    
    private new ParticleSystem particleSystem { get; set; }
    
    private new Collider2D collider { get; set; }
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();
        collider = GetComponent<Collider2D>();

        PlayerStats.Score = 0;
        Armaments = GetComponentsInChildren<GunBehaviour>();
    }
    
    void Update()
    {
        Move();
        Shoot();
        HyperBlink();
    }

    public void SufferDamage(int damage)
    {
        Health -= damage;
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
        rig.velocity = Vector2.zero;
        rig.angularVelocity = 0;
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(Immune());
    }

    private IEnumerator Immune()
    {
        collider.enabled = false;
        Invisible = true;
        yield return ShowBlinkEffect(3);
        collider.enabled = true;
        Invisible = false;
    }
    
    private IEnumerator ShowBlinkEffect(int blinks)
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
        var thrust = Math.Max(Input.GetAxis("Vertical"), 0) * EnginePower;
        particleSystem.Emit(thrust > 0 ? 1 : 0);
        var possibleSpeedUp = (MaxSpeed - rig.velocity.magnitude) * thrust;
        rig.AddRelativeForce(new Vector2(0, Math.Max(possibleSpeedUp, 0) * rig.mass));
        transform.RotateAround(transform.position,Vector3.forward, -torqueVelocity * Input.GetAxis("Horizontal") * Time.deltaTime);
    }

    private void Shoot()
    {
        if (Armaments == null || Armaments.Length <= 0)
        {
            return;
        }
        
        if (Input.GetButton("Fire1"))
        {
            foreach (var arment in Armaments)
            {
                arment.Shoot(rig.velocity * rig.mass);
            }
        }
    }

    private void HyperBlink()
    {
        if (Input.GetButton("Jump"))
        {
            if (!lastBlink.HasValue || lastBlink.Value + HyperBlinkCooldown < Time.time)
            {
                transform.Translate(Camera.main.RandomPoint());
                lastBlink = Time.time;
            }
        }
    }
}
