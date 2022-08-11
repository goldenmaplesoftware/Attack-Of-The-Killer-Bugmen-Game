using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public int currentClip, maxClipSize=10, currentAmmo, maxAmmoSize=100;
    public WeaponManager weapon;

    private void Update()
    {
        if (currentClip > 0 && Input.GetButtonDown("Fire1"))
        { 
            Instantiate(projectile, firePosition.position, firePosition.rotation);
            currentClip--;
        }

        if (Input.GetMouseButtonDown(1))
        {
            int reloadAmount = maxClipSize - currentClip; /// how many bullets to refill clip
            reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
            currentClip += reloadAmount;
            currentAmmo -= reloadAmount;
        }
  


    }



    public void AddAmmo(int ammoAmount) 
    {
        currentAmmo += ammoAmount;
        if(currentAmmo>maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }



}
