using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public static float GameSeconds;
    public static float GameMinutes;

    public static string stringSecond;
    public static string stringMinutes;

    public Text TextTime;

    void Update() {
        GameSeconds = GameSeconds + Time.deltaTime;

        stringSecond = GameSeconds.ToString();
        stringMinutes = GameMinutes.ToString();

        TextTime.text = "Time - " + stringMinutes + ":" + stringSecond.Remove(4);

        if (GameSeconds >= 60.0f) {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        if (GameMinutes >= 24.0f) {
            GameMinutes = 0.0f;
        }
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
