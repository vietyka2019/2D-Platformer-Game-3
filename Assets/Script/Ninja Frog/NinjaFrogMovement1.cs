using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NinjaFrogMovement1 : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;

    private float dirX = 0f;
    private float dirY = 0f;
    MovementState state;

    [SerializeField] float jumpForce = 12f;
    [SerializeField] float doubleJumpForce = 20;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] LayerMask jumpableGround;

    [SerializeField] enum MovementState { idle, run, jump, fall, doubleJump, hit};

    [SerializeField] AudioSource jumpSoundEffect;
    [SerializeField] AudioSource deathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); // stop immediately when we press the button 
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpSoundEffect.Play(); 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
        }

        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        state = MovementState.idle;

        if (dirX > .1f)
        {
            state = MovementState.run;
            spriteRenderer.flipX = false;
        }
        else if (dirX < -.1f)
        {
            state = MovementState.run;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 2f)
        {
            Debug.Log(rb.velocity.y);
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.fall; 
            Debug.Log((int)state);
        }

        if (rb.velocity.y > 14f)
        {
            state = MovementState.doubleJump;
        }

        animator.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        } else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); 
            Die(); 
        }
    }

    private void Die()
    {
        deathSoundEffect.Play(); 
        rb.bodyType = RigidbodyType2D.Static; // disable the hit animation to move
        animator.SetTrigger("DestroyOnCollision");
    }

    private void RestartLevel()
    {
        // Store the index of the current scene in PlayerPrefs
        PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastSceneIndex); // naviage to the game over scnene
    }
}
