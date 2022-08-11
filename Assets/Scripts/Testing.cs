using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private Weapon_Pistol playerAimWeapon;

    private void Start()
    {
        playerAimWeapon.OnShoot += Aim_Pistol_OnShoot;
    }

    private void Aim_Pistol_OnShoot(object sender, Weapon_Pistol.OnShootEventArgs e) 
    {
        UtilsClass.ShakeCamera(0.05f, .2f);
       ///Add more Utils from bullet tracer and particle effect videos
    
    }






}
