using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGraund : MonoBehaviour
{
    public GameObject obj;



    void OnTriggerEnter(Collider col)
    {
        Debug.Log("123");
        if (col.tag == "Player")
        {
           
            obj.SetActive(true);
        }
    }
}
