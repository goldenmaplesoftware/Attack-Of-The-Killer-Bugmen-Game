using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordman_bug : MonoBehaviour
{
    [Header("Attack Paramaters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int damageFlip;
    private int distance = 0;

    [Header("Collider Paramaters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D boxColliderFlip;
    [SerializeField] private float colliderDistance;

    [Header("Player Paramaters")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    /// <summary>
    /// References
    /// </summary>
    private Animator animation;
    private PlayerHealth playerHealth;
    private swordsmanPatrol enemyPatrol;

    /// Health
    [Header("Health")]
     public float HitPoints;
     public float MaxHitPoints=5;
    public healthbarBehavior healthbar;
    public void Start()
    {
        HitPoints = MaxHitPoints;
       
    }


    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        healthbar.SetHealth(HitPoints, MaxHitPoints);

        if (HitPoints <= 0)
        {
            animation.SetTrigger("death_swordman_bug");
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        animation = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<swordsmanPatrol>();
    }


    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        ///Attack when player is in sight

        if (PlayerInSight())
        {
            if (coolDownTimer >= attackCoolDown) ///Attack
            {
                coolDownTimer = 1 ;
                animation.SetTrigger("sword_swing_down");

            }

            if (enemyPatrol != null) ///if you dont see the player keep moving, if you do kill the player
                enemyPatrol.enabled = !PlayerInSight();


        }



        else if (PlayerInSightFlip())
        {
            if (coolDownTimer >= attackCoolDown) ///Attack
            {
                coolDownTimer = 10;
                animation.SetTrigger("flip");
            }


        }

    }


    private bool PlayerInSight() 
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
                new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0,Vector2.left,distance,playerLayer);

        if (hit.collider != null) 
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }



        return hit.collider != null;

        }


    private bool PlayerInSightFlip()
    {
        RaycastHit2D hit2 = Physics2D.BoxCast(boxColliderFlip.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxColliderFlip.bounds.size.x * range, boxColliderFlip.bounds.size.y, boxColliderFlip.bounds.size.z),
        0, Vector2.left, distance, playerLayer);

        if (hit2.collider != null)
        {
            playerHealth = hit2.transform.GetComponent<PlayerHealth>();
        }
        return hit2.collider != null;

    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x* colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.DrawWireCube(boxColliderFlip.bounds.center + transform.right * range * transform.localScale.x* colliderDistance, new Vector3(boxColliderFlip.bounds.size.x * range, boxColliderFlip.bounds.size.y, boxColliderFlip.bounds.size.z));

    }


    private void DamagePlayer() 
    {
        if (PlayerInSight()) 
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void DamagePlayerFlipAttack()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damageFlip);
        }
    }
}
