using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxBehavior : MonoBehaviour
{
    public static event Action<BoxBehavior> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount) 
    {
        health-=damageAmount;
        if (health < -0) 
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }


    }
}
