using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;
    public KeyCode pauseButton;
    [SerializeField]
    private GameObject panelPause;

    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0; 
        }
        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
