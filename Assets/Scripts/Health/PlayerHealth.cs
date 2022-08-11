using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool dead;
    [SerializeField] private float startingHealth;
    public float currentHealth { 
        
        get; 
        private set; 
    }

    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

 

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            ///player is hurt
            anim.SetTrigger("hurt");
            //iframes subframes
        }

        else
        {

            if (!dead)
            {
                ///player is dead
                anim.SetTrigger("die"); ///Cut down

                if(GetComponent<PlayerMovement>()!=null)
                    GetComponent<PlayerMovement>().enabled = false;

                ///Enemy death
                
                ///Swordsman
                if (GetComponent<swordsmanPatrol>() != null)
                    GetComponent<swordsmanPatrol>().enabled = false;

                if (GetComponent<swordman_bug>() != null)
                    GetComponent<swordman_bug>().enabled = false;



                dead = true;
            }
        }


    }
    

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }



}
