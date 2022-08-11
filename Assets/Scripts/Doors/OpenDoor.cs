using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{

    private bool playerDetected;
    public Transform doorPosition;
    public float width;
    public float height;
    public LayerMask whatIsPlayer;
    SceenSwitch sceneSwitch;
    [SerializeField] private string sceneName;

    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceenSwitch>();
    }


    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(width, height), 0, whatIsPlayer);///Creates a box that tells the game the origin point

        if (playerDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("Player contacted door");
                sceneSwitch.SwitchScene(sceneName);
                

            }


        }
    }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(doorPosition.position, new Vector3(width, height, 1));
        }


}










