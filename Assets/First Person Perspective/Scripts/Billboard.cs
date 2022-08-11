using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer theSR;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theSR.flipX=true;
    }

    private void Update()
    {
        transform.LookAt(Player_Controller.instance.transform.position,-Vector3.forward);///This directly accesses the player controller
    }
}
