using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{


    /// Detection Point
    [Header("Detection Paramaters")]
    public Transform detectionPoint;

    /// Detection Radius
    private const float detectionRadius= 1.2f;
    /// Detection Layer
    public LayerMask detectionLayer;
    /// <summary>
    /// Cached Trigger Object
    /// </summary>
    public GameObject detectedObject;

    [Header("Examine Field")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamining;

    [Header("Others")]
    /// <summary>
    /// List of picked up items
    /// </summary>

    public List<GameObject> pickedItems=new List<GameObject>();

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput() 
    {
        return Input.GetKeyDown(KeyCode.P);
    }



    bool DetectObject() 
    {

       Collider2D obj= Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }

        else 
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    */


    public void ExamineItem(Item item)
    {

        if (isExamining)
        {
            examineWindow.SetActive(false); ///This will deactivate the UI for the examination
            isExamining = false;
        }

        else 
        {
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText; ///This will display the description text
            examineWindow.SetActive(true); ///This will activate the UI for the examination
            isExamining = true;
        }
     
    }


}
