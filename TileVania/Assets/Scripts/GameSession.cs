using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    //public static GameSession instance; 
    [SerializeField] int playerLives;
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI scoreText, lifeText;
    void Awake()
    {
        int gameSessions = FindObjectsOfType<GameSession>().Length;
        if(gameSessions>1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        /*if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }*/
    }
    private void Start()
    {
        lifeText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int scoree)
    {
        score += scoree;
        scoreText.text = score.ToString();

    }

    public void PlayerProcessDeath()
    {
        if(playerLives>1)
        {
           Invoke("TakeLife",0.5f);
        }
        else
        {
            Invoke("RestartSession", 0.5f);
        }
    }

    private void TakeLife()
    {
        playerLives--;
        lifeText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        lifeText.text = playerLives.ToString(); //eger canvas tek basýna ise restart olduðundan yeni canvas yukleneceginden bu gozukmeyebilir
                                                //bunun calismasi icin canvasi gameSessionsun child elementi yaptýk bu sekilde burdaki bilgi de gozuktu


    }

    void RestartSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
