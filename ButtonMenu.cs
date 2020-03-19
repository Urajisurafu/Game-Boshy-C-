using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class ButtonMenu : MonoBehaviour
{
    public AudioSource TrackMenuAudioSource;

    public GameObject ButtonOutToMenu;
    public GameObject ButtonStartGame;
    public GameObject ButtonExit;
    public GameObject ButtonRecords;

    public Text ShowName1;
    public Text ShowName2;
    public Text ShowName3;
    public Text ShowName4;
    public Text ShowName5;
    public Text ShowTime1;
    public Text ShowTime2;
    public Text ShowTime3;
    public Text ShowTime4;
    public Text ShowTime5;

    private float [] arr = new float[6];

    public string[] file_name;

    private void Start()
    {
        TrackMenuAudioSource.Play();
        LoadLevel.accountOrder = 0;
    }

    public void StartGame()
    {
        Exit.GameSeconds = 0;
        Exit.GameMinutes = 0;
        Pause.isPaused = false;

        //file_name = File.ReadAllLines("Records.txt");

        SceneManager.LoadScene(1);
    }
    public void Record()
    {
        ButtonOutToMenu.SetActive(true);
        ButtonStartGame.SetActive(false);
        ButtonRecords.SetActive(false);
        ButtonExit.SetActive(false);

        file_name = File.ReadAllLines("Records.txt");

        arr[0] = float.Parse(file_name[1]);
        arr[1] = float.Parse(file_name[3]);
        arr[2] = float.Parse(file_name[5]);
        arr[3] = float.Parse(file_name[7]);
        arr[4] = float.Parse(file_name[9]);
        if (file_name.Length > 10)
            arr[5] = float.Parse(file_name[11]);
        else
            arr[5] = 100000; 
       int check = 0;
       string name;
       float time;

        if (file_name.Length > 10)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (arr[i] > arr[j])
                        check++;
                }
                for (int j = 0; j < 7; j++)
                {
                    time = arr[i];
                    arr[i] = arr[check];
                    arr[check] = time;

                    name = file_name[i * 2];
                    file_name[i * 2] = file_name[check * 2];
                    file_name[check * 2] = name;
                }
                check = 0;
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (arr[i] > arr[j])
                        check++;
                }
                for (int j = 0; j < 7; j++)
                {
                    time = arr[i];
                    arr[i] = arr[check];
                    arr[check] = time;

                    name = file_name[i * 2];
                    file_name[i * 2] = file_name[check * 2];
                    file_name[check * 2] = name;
                }
                check = 0;
            }
        }



        Array.Sort(arr);
        Debug.Log(arr[0]);

        var a = new StreamWriter("Records.txt", false);
        a.Write(file_name[0] + "\r\n" + arr[0] +
            "\r\n" + file_name[2] + "\r\n" + arr[1] +
            "\r\n" + file_name[4] + "\r\n" + arr[2] +
            "\r\n" + file_name[6] + "\r\n" + arr[3] +
            "\r\n" + file_name[8] + "\r\n" + arr[4] + "\r\n");
        a.Close();

        float min = 0;
        float sec = 0;
        for (int i = 0; i < 6; i++)
        {
            min = 0;
            sec = arr[i];
            while (sec - 60 > 0)
            {
                min++;
                sec -= 60; 
            }
            if(i == 0)
                file_name[1] = min.ToString() + ":" + sec.ToString();
            if (i == 1)
                file_name[3] = min.ToString() + ":" + sec.ToString();
            if (i == 2)
                file_name[5] = min.ToString() + ":" + sec.ToString();
            if (i == 3)
                file_name[7] = min.ToString() + ":" + sec.ToString();
            if (i == 4)
                file_name[9] = min.ToString() + ":" + sec.ToString();
        }

        ShowName1.text = file_name[0];
        ShowTime1.text = file_name[1];
        ShowName2.text = file_name[2];
        ShowTime2.text = file_name[3];
        ShowName3.text = file_name[4];
        ShowTime3.text = file_name[5];
        ShowName4.text = file_name[6];
        ShowTime4.text = file_name[7];
        ShowName5.text = file_name[8];
        ShowTime5.text = file_name[9];
    }

    public void OutToMenu()
    {
        ButtonOutToMenu.SetActive(false);
        ButtonStartGame.SetActive(true);
        ButtonRecords.SetActive(true);
        ButtonExit.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
