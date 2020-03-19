using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour
{
    public static int accountOrder = 1;
    private Character character;

    void Update()
    {
        if (Input.GetButtonDown("Rest"))
        {
 
            Character.lives = 5;
            SceneManager.LoadScene(accountOrder);
            Character.isFaint = false;
        }

        if (accountOrder >= 4 || accountOrder == 0)
            accountOrder = 1;

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit is Character)
        {
            Character.lives = 5;
            accountOrder++;
            SceneManager.LoadScene(accountOrder);
        }
    }
}
