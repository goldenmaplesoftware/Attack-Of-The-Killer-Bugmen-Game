using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))] ///This will require the item to possess a box collider

public class Item : MonoBehaviour
{

    ///Interaction Type

    ///Enum is a list of numerical objects and each has an index in the array
    public enum InteractionType 
    {
        NONE,
        PickUp,
        Examine
    
    }

    public enum ItemType
    {
       Static,
       Consumable

    }

    [Header("Attributes")]

    public InteractionType interactType;
    public ItemType type;
    public string descriptionText; ///static item

    [Header("Custom Events")]


    public UnityEvent consumeEvent;
    public UnityEvent customEvent;


    ///Collider Trigger
    public void Reset() ///Sets default values of the object
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact() 
    {
        switch (type) 
        {
            case (ItemType)InteractionType.PickUp:
                ///Add object to the pickedup items list


                gameObject.SetActive(false);
                FindObjectOfType<InventorySystem>().PickUp(gameObject);   ///Looks for an object for a specific type from external script by directly accessing it
                ///Deletes the object
                Debug.Log("Pick up the item");  
                break;
            
            case (ItemType)InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                
                Debug.Log("Examine the item");

                break;

            default:
                Debug.Log("No item");
                break;
        }
    }




}
