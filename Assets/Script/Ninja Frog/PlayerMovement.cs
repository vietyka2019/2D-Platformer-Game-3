using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider2D; 

    private float dirX = 0f;
    private float dirY = 0f;
    [SerializeField] float jumpForce = 14f;
    [SerializeField] float doubleJumpForce = 30; 
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] LayerMask jumpableGround; 

    [SerializeField] enum MovementState { idle, running, jumping, falling, doubleJump, hit};

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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        }

        //dirY = Input.GetAxisRaw("Double Jump");
        //rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);

        UpdateAnimation(); 
    }

    public void UpdateAnimation()
    {
        MovementState state = MovementState.idle;

        if (dirX > .1f)
        {
            state = MovementState.running;
            // spriteRenderer.flipX = false;
            spriteRenderer.flipX = true;
        }
        else if (dirX < -.1f)
        {
            state = MovementState.running;
            //spriteRenderer.flipX = true;
            spriteRenderer.flipX = false;
        }
        else
        {
            // state = MovementState.idle;
            animator.SetBool("isRunning", false);
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("checkState", (int)state);
    }

    private bool IsGrounded() // checks if player collides with jumpableGround.
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}