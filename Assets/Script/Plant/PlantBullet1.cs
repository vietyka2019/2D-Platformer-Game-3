using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet1 : MonoBehaviour
{
    [SerializeField] Animator animator;

    public float speed = 20f;
    public Rigidbody2D rb;
    // PlayerDieEffect playerDieEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // playerDieEffect = new PlayerDieEffect(); 
        // rb.velocity = transform.right * speed;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // playerDieEffect.Die(); 
            Destroy(collision.gameObject); // Destroy the gameObject that has tag equals "Player"
            Destroy(gameObject); // Destroy the bullet 
        }
    }
}