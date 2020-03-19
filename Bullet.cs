using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    private float speed = 10.0f;
    private Vector3 direction;
    public Vector3 Direction {set { direction = value; } }

    public Color Color
    {
        set { sprite.color = value; }
    }

    private SpriteRenderer sprite;

    private void Awake()
    {

        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject,1.4f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ShardWood wood = collider.GetComponent<ShardWood>();
        Unit unit = collider.GetComponent<Unit>();
        Graund graund = collider.GetComponent<Graund>();
        Bullet bullet = collider.GetComponent<Bullet>();
     if (unit && unit.gameObject != parent || wood || graund || bullet)
     {
           // print(collider.gameObject.name);
         Destroy(gameObject);
      }
    }
}
