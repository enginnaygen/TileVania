using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSFx;
    [SerializeField] int scoreIncrease=10;

    bool _wasCollected = false; //bazen coinleri toplarken 1 den fazla algiladigi icin bunu kontrol olarak koyduk


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player" && !_wasCollected)
        {
            _wasCollected = true;
            AudioSource.PlayClipAtPoint(coinSFx, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(scoreIncrease);
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
