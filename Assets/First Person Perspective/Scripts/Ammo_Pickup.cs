using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Pickup : MonoBehaviour
{
    public int ammoAmount = 25;

    private void OnTriggerEnter2D(Collider2D playerAmmoCollision)
    {
        if (playerAmmoCollision.tag == "Player") 
        {
            WeaponManage_FP.instance.currentAmmoACP45 += ammoAmount;
            Destroy(gameObject);
        }
    }
}
