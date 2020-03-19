using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public AudioSource MonsterDamageAudioSource;
    public virtual void ReceiveDamage()
    {
        Die();
        MonsterDamageAudioSource.Play();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);

    }
}
