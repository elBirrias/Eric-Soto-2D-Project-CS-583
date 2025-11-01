using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject fireBallPrefab;

    private Vector2 aimDirection = Vector2.right;

    public float shootCooldown = .5f;
    private float shootTimer;

    public Animator anim;

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        shootTimer -= Time.deltaTime;

        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            //anim.SetBool("isShooting", true);
            Shoot();

        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized; //Avoid faster speed diagonally
            
        }
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        FireBall fireBall = Instantiate(fireBallPrefab, launchPoint.position, Quaternion.identity).GetComponent<FireBall>(); //Quaternion prevents arrow from rotating
        fireBall.direction = aimDirection;
        shootTimer = shootCooldown;
        //anim.SetBool("isShooting", false);
    }
}
