using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //Attach Character to Player Object

public class Player_Controller : MonoBehaviour
{
   
    #region Editor Data
    [Header("Movement Attributes")] //Initialize player speed
    [SerializeField] float _moveSpeed = 50f;
    [SerializeField] int facingDirection = 1;

    [Header("Dependencies")] //Instantiate Rigidbody
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    #endregion

    #region Internal Data
    private Vector2 _moveDir = Vector2.zero; //For initializing movement vector
    #endregion

    #region Tick
    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
        _animator.SetFloat("horizontal", Mathf.Abs(_moveDir.x));
        _animator.SetFloat("vertical", _moveDir.y);
    }

    #endregion

    #region Input Logic
    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");

        if (_moveDir.x > 0 && transform.localScale.x > 0 || _moveDir.x < 0 && transform.localScale.x < 0) //Conditional for sprite flipping
        {
            Flip();
        }

        print(_moveDir);
    }
    #endregion

    #region Movement Logic 
    private void MovementUpdate()
    {
        _rb.velocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime; //Normalized avoids fast diagonal movement
    }
    #endregion

    void Flip()
    {
        facingDirection *= -1;

        //Change game object direction
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

}
