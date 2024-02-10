using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;//2
        int nextSceneIndex = currentScene + 1;//3

        FindObjectOfType<ScenePersist>().ResetScenePersist();

        //   3                   //kac tane Scene oldugu yani burasý da 3, max index degil
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings && collision.gameObject.tag=="Player")
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
