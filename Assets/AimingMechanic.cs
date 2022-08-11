using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingMechanic : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {

        Vector3 gunPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (gunPosition.x < transform.position.x) 
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        
        }

        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);

        }


    }
}
