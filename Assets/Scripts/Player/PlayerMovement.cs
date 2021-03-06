using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource pickupAudioSource;
    AudioSource jumpAudioSource;

    public float speed;
    public int jumpForce;
    public int bounceForce;

    bool coroutineRunning;

    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;


    public AudioClip jumpSFX;
    public AudioMixerGroup soundFXMixer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pickupAudioSource = GetComponent<AudioSource>();
        pickupAudioSource.outputAudioMixerGroup = soundFXMixer;
        pickupAudioSource.loop = false;

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (bounceForce <= 0)
        {
            bounceForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.05f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck not assigned, please assign a value to groundcheck");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);

                if (!jumpAudioSource)
                {
                    jumpAudioSource = gameObject.AddComponent<AudioSource>();
                    jumpAudioSource.outputAudioMixerGroup = soundFXMixer;
                    jumpAudioSource.clip = jumpSFX;
                    jumpAudioSource.loop = false;
                }

                jumpAudioSource.Play();

            }

            Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
            rb.velocity = moveDirection;

            anim.SetFloat("speed", Mathf.Abs(horizontalInput));
            anim.SetBool("isGrounded", isGrounded);


            if (sr.flipX && horizontalInput > 0 || !sr.flipX && horizontalInput < 0)
                sr.flipX = !sr.flipX;


            if (Input.GetKeyDown(KeyCode.B))
            {
                GameManager.instance.lives--;
            }

        }
    }

    public void StartJumpForceChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine("JumpForceChange");
        }
        else
        {
            StopCoroutine("JumpForceChange");
            StartCoroutine("JumpForceChange");
        }
    }

    IEnumerator JumpForceChange()
    {
        coroutineRunning = true;
        jumpForce = 600;
        //change variable above this line to new jump force value.
        yield return new WaitForSeconds(5.0f);
        //change variable back under this line to default jump force value.
        jumpForce = 300;
        coroutineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Squish")
        {
            collision.gameObject.GetComponentInParent<EnemyWalker>().IsSquished();
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * bounceForce);
            Destroy(collision.gameObject);
        }
    }

    public void CollectibleSound(AudioClip pickupAudio)
    {
        pickupAudioSource.clip = pickupAudio;
        pickupAudioSource.Play();
    }
}
