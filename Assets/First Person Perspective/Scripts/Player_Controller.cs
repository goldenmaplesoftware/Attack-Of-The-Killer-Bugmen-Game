using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller instance;/// <summary>
    ///  This object can be referenced to any version of this script
    /// </summary>
    public Rigidbody2D playerBody;
    public Camera viewPlayerCamera;
    [SerializeField] public float moveSpeed=5f;
    [SerializeField]public float mouseSensitivity=1;
    private Vector2 moveInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        instance = this;    
    }


    void Update()
    {
        ///Movement of the player
        ///

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveHorizontal = transform.up * -moveInput.x;  ///This takes upward movement and multiplies it by the x value
        Vector3 moveVertical = transform.right * moveInput.y;  ///This takes side to side movement and multiplies it by the x value

        playerBody.velocity = (moveHorizontal + moveVertical) * moveSpeed;




        ///Player control view
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"))*mouseSensitivity;
        ///This controls horizontal mouse view
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z-mouseInput.x);///The rotation of X,Y,Z,W Quaternion is 4 dimesional mesaurement accessed by debug that converts to 3D                                                                                                                                                  ///This controls vertical mouse view
        viewPlayerCamera.transform.localRotation = Quaternion.Euler(viewPlayerCamera.transform.localRotation.eulerAngles + new Vector3(0f,mouseInput.y,0f));
    }


}
