using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Instantiate(projectile, firePosition.position, firePosition.rotation);
        }
    }

}
