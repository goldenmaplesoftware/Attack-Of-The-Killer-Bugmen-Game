using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BugSoliderStandard : MonoBehaviour
{
    [Header("Attack Paramaters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

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
    [SerializeField] public float HitPoints;
    [SerializeField] public float MaxHitPoints = 8; /// Default, but can vary
   
    public healthbarBehavior healthbar;
    public void Start()
    {
        HitPoints = MaxHitPoints;
        healthbar.SetHealth(HitPoints, MaxHitPoints);
        

    }

    float delay;
    System.Action action;

 
    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        animation.SetTrigger("hurt_bugSolider");
        healthbar.SetHealth(HitPoints, MaxHitPoints);

        if (HitPoints <= 0) ///This is the death condition once HP<=0
        {
            animation.SetTrigger("death_bugSolider");
            
            if(GetComponent<BugSoliderStandard>()!=null)
                GetComponent<BugSoliderStandard>().enabled = false;
            
            if (GetComponentInParent<BugSoliderPatrol>() != null)
                GetComponentInParent<BugSoliderPatrol>().enabled = false;
           
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
                coolDownTimer = 1;
                animation.SetTrigger("shooting_attack_bugSolider");

            }

            if (enemyPatrol != null) ///if you dont see the player keep moving, if you do kill the player
                enemyPatrol.enabled = !PlayerInSight();


        }



    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, distance, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }



        return hit.collider != null;

    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.DrawWireCube(boxColliderFlip.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxColliderFlip.bounds.size.x * range, boxColliderFlip.bounds.size.y, boxColliderFlip.bounds.size.z));

    }


    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

}
