using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public AudioSource HeartAudioSource;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character && character.Lives < 5)
        {
     //       Debug.Log(character.Lives);
            character.Lives++;
            HeartAudioSource.Play();
            Destroy(gameObject);
        }
    }
}
