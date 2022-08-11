using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class EffectScript : MonoBehaviour
{
    [SerializeField] private Aim_Pistol playerAimPistol;

    private void Start()
    {
        System.EventHandler<Aim_Pistol.OnShootEventArgs> Aim_Pistol_OnShoot = null;
        playerAimPistol.OnShoot += Aim_Pistol_OnShoot;

    }

    private void Aim_Pistol_OnShoot(object sender, Aim_Pistol.OnShootEventArgs e) 
    {
        UtilsClass.ShakeCamera(1f, 2f);
    
    }



}
