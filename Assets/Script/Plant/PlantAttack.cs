using System.Collections;
using UnityEngine;

public class PlantAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform plantPos;
    [SerializeField] Transform playerPos;
    [SerializeField] float speed;
    [SerializeField] LayerMask layerMask;
    Vector2 direction;
    RaycastHit2D hit; 

    private bool canAttack = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(plantPos.position, Vector2.right, 50);

        Debug.DrawRay(plantPos.position, -plantPos.right * 100, Color.red);

        if (hit.collider.CompareTag("Player") && canAttack)
        {
            Attack(); 
        } else
        {
            Debug.Log("Not find");
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            animator.SetTrigger("attack");
            Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
            canAttack = false;
            StartCoroutine(ResetAttack());
        } 
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1); // Wait for 1 second
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hello"); 
            // Xóa character khỏi màn hình game
            Destroy(gameObject);
        } else
        {
            Debug.Log("Hello!");
        }

    }
}