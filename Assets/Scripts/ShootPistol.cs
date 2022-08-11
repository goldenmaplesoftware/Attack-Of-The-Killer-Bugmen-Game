using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPistol : MonoBehaviour
{
    [SerializeField] private float pistolCoolDown;
    private float coolDownTimer=Mathf.Infinity;
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)&&coolDownTimer>pistolCoolDown&&playerMovement.canAttack()) 
        {
            Attack();
            coolDownTimer += Time.deltaTime;
        }
    }

    private void Attack() 
    {
        anim.SetTrigger("Active");
        coolDownTimer = 0;
    }

}
