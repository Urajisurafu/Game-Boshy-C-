using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graund : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider){
        Unit unit = collider.GetComponent<Unit>();
    }
}
