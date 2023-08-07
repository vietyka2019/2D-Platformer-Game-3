using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCharacterOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            //state = MovementState.hit;
            // animator.SetInteger("state", (int)state);
            Destroy(gameObject);
        }
    }
}
