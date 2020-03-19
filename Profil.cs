using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;

public class Profil : MonoBehaviour
{

    public GameObject Info;
    public GameObject ShowName;

    public InputField InName;

    public Text ShowTextName;
    public Text ShowTime;

    float size;//перевод в число времени

    void Start()
    {
        ButtonMenu buttonMenu = GetComponent<ButtonMenu>();
        ShowTime.text = "Time - " + Exit.stringMinutes + ":" + Exit.stringSecond.Remove(4);
        LoadLevel.accountOrder = 4;
        
        if(float.Parse(Exit.stringMinutes) > 0)
            size = float.Parse(Exit.stringMinutes) * 60 + float.Parse(Exit.stringSecond);
        else
            size =  float.Parse(Exit.stringSecond);

    }

    public void Reset()
    {
        InName.text = string.Empty;
        Info.SetActive(true);
        ShowName.SetActive(false);
    }

    public void Next()
    {
        Info.SetActive(false);
        ShowName.SetActive(true);
        ShowTextName.text = InName.text;
    }

    public void Save()
    {

        string textName = ShowTextName.text;
        string textTime = Exit.stringMinutes + ":" + Exit.stringSecond.Remove(4);

        var a = new StreamWriter("Records.txt", true);
        a.Write(textName + "\r\n" + Convert.ToString(size));
        a.Close();

        SceneManager.LoadScene(0);
    }
}
