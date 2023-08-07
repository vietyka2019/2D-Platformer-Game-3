using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrogMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jump = 0f;
    [SerializeField] float doubleJump = 0f;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer; 

    const float GROUND_CHECK_RADIUS = 0.2f; 
    private bool isGrounded = false;
    private SpriteRenderer sprite; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetBool("checkRun", false);
        Vector3 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) // run to the right
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("checkDoubleJump", false);
            inputVector.x = 1;
            sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.A)) // run to the left
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("checkDoubleJump", false);

            inputVector.x = -1;
            sprite.flipX = true; 
        }

        if (Input.GetKey(KeyCode.W)) // jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            // rb.AddForce(new Vector2(0f, jump));
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("checkDoubleJump", false);
        }

        if (Input.GetKey(KeyCode.Space)) // double jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            animator.SetBool("checkDoubleJump", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            animator.SetBool("checkDoubleJump", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
        }

        animator.SetFloat("yVelocity", rb.velocity.y); 

        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);
        this.transform.position += moveDir * Time.deltaTime * moveSpeed;
    }

    private void FixedUpdate()
    {
        GroundCheck(); 
    }

    void GroundCheck()
    {
        isGrounded = false;

        // Check if the GroundCheck obj is colliding with other 
        // 2d colliders that's are in the ground layer 
        // If yes (isGround true) else (isGround  false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, GROUND_CHECK_RADIUS, groundLayer);
        if (colliders.Length > 0)
        {
            // Debug.Log("Hello");
            isGrounded = true; 
        }

        // animator.SetFloat("Jump");
        //if (isGrounded == false)
        //{
        //    Debug.Log("Ahih");
        //}
    }
}
