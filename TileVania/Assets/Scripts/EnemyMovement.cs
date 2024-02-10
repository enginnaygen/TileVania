using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed=2f;
    Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision) //buradaki mantýk su sekilde enemy duvara yaklasýyor yaklasýyor
    {                                                  //daha sonra isTriggerlý box colliderin on kýsmý duvara  
        moveSpeed *= -1;                               //carpýyor, duvarýn icinden geciyor ve icinden gectikten 
        FlipEnemy();                                   //sonraki an onTriggerExit calisiyor 
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }
}
