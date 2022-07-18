using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;
    private float timeToSwitch = 10f;
    private float timer;
    private int step;
    void Start()
    {
        timer = timeToSwitch;
        step = 1;
    }
    void FixedUpdate()
    {
        if (timer < 0)
        {
            NextStep();
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }
    private void NextStep()
    {
        switch (step)
        {
            case 1:
                step++;
                timer = timeToSwitch;
                img1.SetActive(false);
                img2.SetActive(true);
                break;
            case 2:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
