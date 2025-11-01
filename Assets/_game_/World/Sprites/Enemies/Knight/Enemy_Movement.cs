using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    public float speed;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1; //Check which side is facing the player
    private EnemyState enemyState; //Track current state

    

    private Rigidbody2D rb;
    private Transform player; //Reference to know where to chase the player
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        //Tell enemy what its rigid body  and animator component are
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (enemyState == EnemyState.Chasing)
        {
            Chase();

        } else if(enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero; //Stop moving when attacking
        }
    }

    void Chase()
    {
        //If player is on the right or left
        if (player.position.x > transform.position.x && facingDirection == -1 || player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        //Difference in positions to know distance and where to go
        Vector2 direction = (player.position - transform.position).normalized; //If not normalized,some values might affect speed
        rb.velocity = direction * speed;
    }

    void Flip()
    {

        facingDirection *= -1;
        //Flip the enemy game object
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer() //2D Collider functions as a field of view/detection
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0) //Found player
        {
            player = hits[0].transform;

            //Attack if distance between player and enemy is within attack range

            if (Vector2.Distance(transform.position, player.position) < attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);


            }
            //Switch to chase if not within attack range
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                ChangeState(EnemyState.Chasing); //Change animation state
            }
        }
        
        else //If player is out of sight
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
        
    }

    void ChangeState(EnemyState  newState)
    {

        //Change states depending on action

        //Exit current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
           
        } else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isMoving", false);
        }

        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        //Update current animation
        enemyState = newState;

        //Enter current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);

        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isMoving", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }

    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}
