using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Vector2 lastCheckpointPos;
    [SerializeField] private GameObject loaderCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(/*sceneName*/SceneManager.GetActiveScene().buildIndex + 1);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            //do some progress bar stuff
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }
}
