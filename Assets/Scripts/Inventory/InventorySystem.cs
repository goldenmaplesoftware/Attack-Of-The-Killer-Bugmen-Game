using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Field")]
    ///Item list of picked up items
    public List<GameObject> items=new List<GameObject>();
    /// <summary>
    /// Indicates if the inventory is open or not
    /// </summary>
    public bool isOpen;
    [Header("UI Items Selection")]
    public GameObject UI_Window;
    public Image[] items_images;
    [Header("UI Items Description Section")]
    public GameObject UI_DescriptionWindow;
    public Image description_Image; //image of item
    public Text description_Title;///title of item
    public Text description_About;///about item

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
             
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        UI_Window.SetActive(isOpen);
        Update_UI();

    }
    public void PickUp(GameObject item) 
    {
        items.Add(item);
        Update_UI();
       
    }

    /// <summary>
    /// Refresh elements in UI window
    /// </summary>
    void Update_UI()
    {
        HideAll();
        ///Each item in the items list,  display it in the respective slot
        for (int i = 0; i < items_images.Length; i++)
        {
            items_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        
        }
    }

    /// <summary>
    /// Hide all of the item images
    /// </summary>
    void HideAll() 
    {

        foreach (var i in items_images)

        {
            i.gameObject.SetActive(false);
        }
        HideDescriptionTitle();

    }

    public void ShowDescriptionTitle(int id) 
    {
        ///Set image
        description_Image.sprite = items_images[id].sprite;
        ///Set text 
        description_Title.text = items[id].name;
        description_About.text = items[id].GetComponent<Item>().descriptionText;

        ///Show elements
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_About.gameObject.SetActive(true);

    }

    public void HideDescriptionTitle()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_About.gameObject.SetActive(false);
    }


    public void Consume(int id)
    {
        if (items[id].GetComponent<Item>().type == Item.ItemType.Consumable) 
        {
            Debug.Log("Consumed"+items[id].name);
            items[id].GetComponent<Item>().consumeEvent.Invoke(); ///invokes the consume event
                                                                  ///Clear item from list
                                                                  /// ///Destroy the item
            Destroy(items[id], 0.1f);  ///item to destroy and time interal

            ///items.Remove(items[id]);
            items.RemoveAt(id);
            ///Update UI
            Update_UI();
           
            
        }
    
    }


}
