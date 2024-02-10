using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//buralarda hata yapmadým, confiner ile carpýstýgý ýcýn bullet yukarý assa gidip duruyordu, layer matrixinden bu durumu cozdum
public class Bullet : MonoBehaviour
{
    Rigidbody2D _bulletRb;
    PlayerMovement _player;
    [SerializeField] float bulletSpeed;
    float _xSpeed;
    void Start()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * bulletSpeed;
        _bulletRb.velocity = new Vector2(_xSpeed, 0f);
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        Destroy(this.gameObject);
    }

}
