using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField ]public float projectileSpeed;
    private Rigidbody2D rigidbody;
    private Animator anim;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

         Destroy(this.gameObject, 1);
        if (collision.gameObject.TryGetComponent<swordman_bug>(out swordman_bug enemy))
        {
            enemy.TakeDamage(1);
        }

        else if (collision.gameObject.TryGetComponent<BugSoliderStandard>(out BugSoliderStandard enemy2))
        {
            enemy2.TakeDamage(1);
        }
    }
}
