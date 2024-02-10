using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 _moveInput;
    Rigidbody2D _rb;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] float deadJump = 8f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Animator _animator;
    CapsuleCollider2D _capsuleCollider;
    BoxCollider2D _boxCollider;
    float _gravity;
    bool _isAlive = true;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gravity = _rb.gravityScale;
    }

    void Update()
    {
        if (!_isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!_isAlive) return;
        _moveInput = value.Get<Vector2>();

    }

    void OnJump(InputValue value)
    {
        if (!_isAlive) return;
        if (!_boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * runSpeed, _rb.velocity.y);
        _rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;

        _animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {

        if (_boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbingVelocity = new Vector2(_rb.velocity.x, _moveInput.y * climbSpeed);
            _rb.velocity = climbingVelocity;
            _rb.gravityScale = 0;

            bool playerHasVerticalSpeed = Mathf.Abs(_rb.velocity.y) > Mathf.Epsilon;

            _animator.SetBool("isClimbing", playerHasVerticalSpeed);


            /*if (_rb.velocity.y != 0)
            {
                _animator.SetBool("isClimbing", true);
            }
            else if(_rb.velocity.y ==0)
            {
                _animator.SetBool("isClimbing", false);

            }*/
        }
        else
        {
            _animator.SetBool("isClimbing", false);
            _rb.gravityScale = _gravity;

        }


    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Die();
        }
    }*/

    void Die()
    {
        if (_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) ||
           _boxCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) //burayi bunu eklemek yerine
                                             //_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards))
        {                                    //seklinde yaparak ve obstacle tilemap in tile colliderini isTrigger  
                                                                            //yaparsak da olur                 
            FindObjectOfType<GameSession>().PlayerProcessDeath(); //en baþtan referans vermememizin sadece burada cagirmamizin sebebi, her Scenede farklý bir 
            _isAlive = false;            //gameSession var, eger ilk scene de bastan referans verirsek sikinti oluyor  
            _animator.SetTrigger("death");
            _rb.velocity = new Vector2(_rb.velocity.x, deadJump);

            //_capsuleCollider.enabled = false;
            //_boxCollider.enabled = false;
            //_rb.mass = 0;
        }
    }

    void OnFire(InputValue value)
    {
        if (!_isAlive) return;
        Instantiate(bullet, gun.position, transform.rotation);
        /*if (value.isPressed)
        {
           
        }*/
    }
}