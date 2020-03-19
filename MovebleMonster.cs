using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MovebleMonster : Monster
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

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7f)
            ReceiveDamage();
            else
                unit.ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.53f, 0.1f);
        Collider2D[] colliderdown = Physics2D.OverlapCircleAll(transform.position + transform.up * -0.05f  , -0.1f);
        if (colliders.Length > 0 && colliders.All(x=> !x.GetComponent<Character>()) && colliders.All(x => !x.GetComponent<Bullet>()) && colliders.All(x => !x.GetComponent<Heart>()) && colliders.All(x => !x.GetComponent<Monster>()))
            direction *= -1.0f;
        if (colliderdown.Length > 0)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction , speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction + fall * 1.1f, speed * Time.deltaTime);


    }
}
