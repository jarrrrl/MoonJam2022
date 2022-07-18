using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishGame : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) EndGame();
    }

    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("Cutscene").buildIndex);

    }
}
