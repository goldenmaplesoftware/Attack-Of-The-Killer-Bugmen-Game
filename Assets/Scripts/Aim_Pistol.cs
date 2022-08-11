using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Aim_Pistol : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs 
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;

    
    }



    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;



  

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = transform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform= aimTransform.Find("ShellPosition");
    }


    private void Update()
    {
        HandleAiming();
        HandleShooting();

    }

    private void HandleAiming() ///This handles the pistol aiming mechanics
    { 
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        ///Debug.Log(angle);
        ///
     
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < 180)
        {
            localScale.x = 1f;
        }

        else
        {
            localScale.x = -1f;
           
        }

        aimTransform.localScale=localScale;
        
    }

    private void HandleShooting() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            aimAnimator.SetTrigger("Shoot"); ///This inititates the pistol shooting animation
            OnShoot?.Invoke(this,new OnShootEventArgs 
            { 
            
                gunEndPointPosition= aimGunEndPointTransform.position,
                shootPosition=mousePosition,
                shellPosition = aimShellPositionTransform.position,


            });
        }
    }



}



