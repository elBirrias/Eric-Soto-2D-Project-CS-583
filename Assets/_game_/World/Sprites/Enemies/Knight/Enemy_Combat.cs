using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;

    public AudioClip attackSound;
    private AudioSource audioSource;

    //private void OnCollisionEnter2D(Collision2D collision) //Damage on contact
    //{
    //    // Check if the object we hit is tagged "Player"
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // Now that we know it's the player, try to get the PlayerHealth component
    //        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

    //        // Extra safety check: Make sure the component was actually found
    //        if (playerHealth != null)
    //        {
    //            playerHealth.ChangeHealth(-damage);
    //        }
    //    }
    //}

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {

        audioSource.PlayOneShot(attackSound);
        //This collider checks if there is an object within the weapon range at the attack point, at the player layer
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if(hits.Length > 0) //If a player is found, deal damage
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }

    
}