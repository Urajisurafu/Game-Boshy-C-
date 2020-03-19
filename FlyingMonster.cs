using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyingMonster : Monster
{
    [SerializeField]
    private float speed = 2.0f;


    private Vector3 direction;
    private Vector3 fall;

    private bool isBirdDie = false;

    private int FlyingMonsterLives = 1;
    private Bullet bullet;
    private Character character;

    private SpriteRenderer sprite;
    private Animator animator;

 
    private StateBird State
    {
        get { return (StateBird)animator.GetInteger("State"); }
        set { animator.SetInteger("State",(int) value); }

    }

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        character = FindObjectOfType<Character>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        direction = transform.right;
    }

    protected override void Update()
    {
        if(!isBirdDie)
            State = StateBird.Fly;
        else
            State = StateBird.Die;

        Move();
       /* if(transform.position.y < -10)
            ReceiveDamage();*/
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (FlyingMonsterLives <= 0)
            ReceiveDamage();

        if (bullet)
        {
            isBirdDie = true;
           // ReceiveDamage();
        }
        if (unit && unit is Character && !isBirdDie)
        {
            unit.ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.53F, 0.1f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>()) && colliders.All(x => !x.GetComponent<Bullet>()) && colliders.All(x => !x.GetComponent<Heart>()))
            direction *= -1.0f;
        if (!isBirdDie)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            sprite.flipX = direction.x < 0.0f;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, transform.position  + transform.up * -2F, speed * Time.deltaTime);

    }
}

public enum StateBird
{
    Fly,
    Die
}
