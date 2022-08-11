using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AK47 : MonoBehaviour
{
    public Animator gunAK47Anim;

    public int currentAmmo_AK47 = 30;

  
    // Update is called once per frame
    void Update()
    {
       


        if (Input.GetMouseButtonDown(0)) ///fire shot
        {
            if (WeaponManage_FP.instance.currentAmmoAK47 > 0)
            {
                Ray bulletRayProduce = Player_Controller.instance.viewPlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit bulletRayCollision;
                if (Physics.Raycast(bulletRayProduce, out bulletRayCollision))
                {
                    ///Debug.Log("I am look at" + bulletRayCollision.transform.name); ///this checks if bullet hits anything
                    Instantiate(WeaponManage_FP.instance.bulletImpact, bulletRayCollision.point, transform.rotation);
                }
                else
                {
                    Debug.Log("I am not looking at nothing");
                }
                gunAK47Anim.SetTrigger("Fire_AK47");
                WeaponManage_FP.instance.currentAmmoAK47--;

            }
        }
    }
}
