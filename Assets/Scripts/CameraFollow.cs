using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;


public class CameraFollow : MonoBehaviour
{


    private Func<Vector3> GetCameraFollowPositonFunction;
    public void Setup(Func<Vector3> GetCameraFollowPositonFunction)
    {
        this.GetCameraFollowPositonFunction = GetCameraFollowPositonFunction;
    }

    public void SetGetCameraFollowPositionFunction(Func<Vector3> GetCameraFollowPositionFunction) 
    {
        this.GetCameraFollowPositonFunction = GetCameraFollowPositionFunction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositonFunction();
        cameraFollowPosition.z = transform.position.z;
        
        Vector3 camerMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2.5f;


        if (distance > 0) 
        {
            Vector3 newCamaeraPosition = transform.position + camerMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCamaeraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                ///Overshoots the target
                newCamaeraPosition = cameraFollowPosition;
            }

            transform.position = newCamaeraPosition;

        }

        

    }
}
