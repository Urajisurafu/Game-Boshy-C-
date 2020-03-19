using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;

    [SerializeField]
    private Transform target;


    public AudioSource Track1AudioSource;

    private void Awake()
    {
        Boss boss = GetComponent<Boss>();
        Character character = GetComponent<Character>();
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    private void Start()
    {
        Track1AudioSource.Play();
    }

    private void Update()
    {
        
        Vector3 position = target.position;
            position.z = -10.0f;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        CharacterCheck();

    }
    private void CharacterCheck()
    {
        if(Boss.isBossDie)
            Track1AudioSource.Pause();

        if (Character.isFaint)
        {
            speed = 0;
            Track1AudioSource.Pause();
        }
        if (Pause.isPaused)
        {
            Track1AudioSource.Pause();
        }
        if (!Pause.isPaused && !Character.isFaint)
        {
            Track1AudioSource.UnPause();
        }
    }
}
