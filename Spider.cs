using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spider : Monster
{
    [SerializeField]
    private float speed = 2.0f;


    private Vector3 direction;
    private Vector3 fall;

    private Bullet bullet;
    private Character character;

    private SpriteRenderer sprite;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        character = FindObjectOfType<Character>();
    }

    protected override void Start()
    {
        direction = transform.right;
        fall = transform.up * -1;
    }

    protected override void Update()
    {
        // if (character.CharacterPosition - transform.position.x <= System.Math.Abs(30) )
        if (System.Math.Abs(transform.position.x - character.CharacterPosition) <= System.Math.Abs(30))
            Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Character character = collider.GetComponent<Character>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            ReceiveDamage();
        }
        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
        }

    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.53f, 0.1f);
        Collider2D[] colliderdown = Physics2D.OverlapCircleAll(transform.position + transform.up * -0.05f, -0.1f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>()) && colliders.All(x => !x.GetComponent<Bullet>()) && colliders.All(x => !x.GetComponent<Heart>()) && colliders.All(x => !x.GetComponent<Monster>()))
            fall *= -1.0f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + fall, speed * Time.deltaTime);



    }
}
