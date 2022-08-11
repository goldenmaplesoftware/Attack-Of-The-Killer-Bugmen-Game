using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralex : MonoBehaviour
{
   
    private float length,startposition;
    public GameObject camera;
    public float parallexEffect;



    void Start()
    {
        startposition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (camera.transform.position.x *(1-parallexEffect));
        float distance = (camera.transform.position.x * parallexEffect);

        transform.position = new Vector3(startposition + distance, transform.position.y, transform.position.z);

        if (temp > startposition + length) startposition += length;
        else if (temp < startposition - length) startposition -= length;
    }
}
