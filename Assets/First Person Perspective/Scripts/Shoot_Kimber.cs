using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot_Kimber : MonoBehaviour
{
    public Animator gunKimberPistolAnim;

     public int currentAmmo_45ACP=5;
    // Update is called once per frame
    void Update()
    {




        if (Input.GetMouseButtonDown(0)) ///fire shot
        {
            if (WeaponManage_FP.instance.currentAmmoACP45 > 0)
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
                gunKimberPistolAnim.SetTrigger("FireKimber");
                WeaponManage_FP.instance.currentAmmoACP45--;
                
            }
        }
    }
}
