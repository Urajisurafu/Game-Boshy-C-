using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Monster
{
    [SerializeField]
    private float speed = 2.0f;


    private Vector3 direction;
    private Vector3 fall;

    public static bool isBossDie = false;

    public GameObject emptu;

    public float HP = 100;
    public Image UIHP;

    private int FlyingMonsterLives = 1;
    private Bullet bullet;
    private Character character;

    private SpriteRenderer sprite;
    private Animator animator;


    private StateBird State
    {
        get { return (StateBird)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }

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
        direction = -transform.right;
        fall = -transform.up;
    }

    protected override void Update()
    {
        UIHP.fillAmount = HP / 100;

        if (HP <= 0) {
            isBossDie = true;
        }

        if (!isBossDie)
            State = StateBird.Fly;
        else
            State = StateBird.Die;

        if (System.Math.Abs(transform.position.x - character.CharacterPosition) <= System.Math.Abs(11))
            Move();

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Bullet bullet = collider.GetComponent<Bullet>();


        if (bullet)
        {
                HP = HP - 5;
            // ReceiveDamage();
        }
        if (unit && unit is Character && !isBossDie)
        {
            unit.ReceiveDamage();
        }
        if (HP <= 0)
        {
            Destroy(emptu);
            isBossDie = true;
            ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.53F, 0.1f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>()) && colliders.All(x => !x.GetComponent<Bullet>()) && colliders.All(x => !x.GetComponent<Heart>()))
            direction *= -1.0f;


        if (!isBossDie)
        {

            if (transform.position.y < 2)
                fall *= -1.0f;
            if (transform.position.y > 4)
                fall *= -1.0f;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction + fall, speed * Time.deltaTime);
            sprite.flipX = direction.x > 0.0f;
        }
        if (isBossDie)
        {
 
            transform.position = Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
        }

    }
}
