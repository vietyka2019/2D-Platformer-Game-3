using System.Collections;
using UnityEngine;

public class PlantAttack3 : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform plantPos;
    [SerializeField] float speed;
    [SerializeField] LayerMask layerMask;
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

        Debug.DrawRay(plantPos.position, plantPos.right * 100, Color.red);

        if (hit.collider.CompareTag("Player") && canAttack)
        {
            Attack();
        }
        else
        {
            animator.SetBool("attack", false);
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
        yield return new WaitForSeconds(1.75f); // Wait for 1 second
        canAttack = true;
    }
}