using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{

    public static int lives = 5;
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float jumpForce = 15.0f;

    public float characterPosition = 0;

    public float CharacterPosition
    {
        get { return characterPosition; }
    }

    public int Lives
    {
        get { return lives; }
        set { if (value <= 5) lives = value;
            livesBar.Refresh();
        }
    }
    Lives livesBar;

    bool checkDie = false;
    private bool isGrounded = false;
    private bool isDamage = false;
    public static bool isFaint = false;

   // private AudioSource CharacterJump;
    public AudioSource DamageAudioSource;
    public AudioSource JumpAudioSource;
    public AudioSource DieAudioSource;

    private Bullet bullet;
    private CharState State
    {
    get {return (CharState)animator.GetInteger("State");}
        set { animator.SetInteger("State", (int)value); }

    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private void Awake()
    {
        livesBar = FindObjectOfType<Lives>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    //    JumpAudioSource = GetComponent<AudioSource>();
    //    DamageAudioSource = GetComponent<AudioSource>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        characterPosition = transform.position.x;
        if (isGrounded && !isFaint)
        {
            State = CharState.Idle;
            isDamage = false;
        }
            if (Input.GetButton("Horizontal") && !isFaint && !Pause.isPaused)
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") && !isDamage && !isFaint && !Pause.isPaused)
            Jump();
        if (Input.GetButtonDown("Fire1") && !isFaint && !Pause.isPaused)
            Shoot();
        if (isDamage && !isFaint )
        {
            State = CharState.Dizzy;
            //isDamage = true;
        }
        if (isFaint)
        {
            State = CharState.Faint;
        }

        if (Pause.isPaused)
        {
            DamageAudioSource.Pause();
            JumpAudioSource.Pause();
            DieAudioSource.Pause();
        }
        else
        {
            DamageAudioSource.UnPause();
            JumpAudioSource.UnPause();
            DieAudioSource.UnPause();
        }
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.9f;

        Bullet newBullet = Instantiate(bullet, position,bullet.transform.rotation);

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction,speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0f;
        
        if (isGrounded)
            State = CharState.Run;
    }
    private void Jump()
    {
        rigidbody.AddForce(transform.up* jumpForce, ForceMode2D.Impulse);//прикладывание силы
        State = CharState.Jump;
        JumpAudioSource.Play();
    }

    public override void ReceiveDamage()
    {
      
        isDamage = true;
        // State = CharState.Dizzy;
        Lives--;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        rigidbody.velocity = Vector3.zero;
       
        if(!isFaint)
        {
            DamageAudioSource.Play();
            if (direction.x < 0.0f)
        {
            rigidbody.AddForce(transform.right * 2.0f, ForceMode2D.Impulse);
            rigidbody.AddForce(transform.up * 10.0f, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.AddForce(transform.right * -2.0f, ForceMode2D.Impulse);
            rigidbody.AddForce(transform.up * 10.0f, ForceMode2D.Impulse);
        }
        }
        if (Lives == 0)
        {
            CharacterDie();
        }
       // Debug.Log(lives);
    }

    private void CharacterDie()
    {
        isFaint = true;
        State = CharState.Faint;
        DieAudioSource.Play();
        checkDie = true;
    }
    private void CheckGround()
    {
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,0.3f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded && !isDamage)
            State = CharState.Jump;
        if (transform.position.y < -10)
        {
            Lives = 0;
            if (!checkDie)
            {
                CharacterDie();
               
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
    
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump,
    Dizzy,
    Faint
}