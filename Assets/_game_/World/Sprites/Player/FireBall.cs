using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.left;
    public float lifeSpawn = 2;
    public float speed;
    public int damage;

    public LayerMask enemyLayer;

    void Start()
    {
        rb.velocity = direction * speed;
        Destroy(gameObject, lifeSpawn);
    }

  public void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if layers overlap in order to deal damage
        if((enemyLayer.value & (1 << collision.gameObject.layer)) > 0 )
        {
            //Deal damage
            collision.gameObject.GetComponent<Enemy_Health>().ChangeHealth(-damage);
            Destroy(gameObject);

        }
    }
}
