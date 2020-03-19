using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{

    [SerializeField]
    public int ShootableMonsterLives = 3;
    private float rate = 2.0f;
    private Bullet bullet;
    private Character character;

    [SerializeField]
    private Color bulletColor = Color.white;


    private SpriteRenderer sprite;

    protected override void Awake()
    {
        character = FindObjectOfType<Character>();
        bullet = Resources.Load<Bullet>("Bullet");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    //private  void Update()
    protected override void Update()
    {
        Turn();
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }
    private void Shoot()
    {

        Vector3 position = transform.position;
        position.y += 0.7f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        if(character.CharacterPosition < transform.position.x)
        newBullet.Direction = -newBullet.transform.right; // немного изменить поворо
        else
            newBullet.Direction = newBullet.transform.right;
        newBullet.Color = bulletColor;//цвет пули
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        Character character = collider.GetComponent<Character>();
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet && bullet.Parent != gameObject )
        {
            ShootableMonsterLives--;
        }
        if(ShootableMonsterLives <= 0)
            ReceiveDamage();
        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7f)
                ReceiveDamage();
            else
                unit.ReceiveDamage();
        }
    }

    private void Turn()
    {
        Vector3 direction = transform.right;

        transform.position = Vector3.MoveTowards(transform.position, transform.position, Time.deltaTime);
        if (transform.position.x >= 1)
        {
            if (character.CharacterPosition < transform.position.x)
                sprite.flipX = -direction.x < -character.CharacterPosition;
            else
                sprite.flipX = direction.x < character.CharacterPosition;
        }
        else
        {
            if (character.CharacterPosition < transform.position.x)
                sprite.flipX = -direction.x < character.CharacterPosition;
            else
                sprite.flipX = direction.x < -character.CharacterPosition;
        }

    }

}
