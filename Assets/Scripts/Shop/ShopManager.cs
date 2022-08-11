using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public int kyro;
    public TMP_Text kyroUI;
    public ShopItem[] shopItems;
    public GameObject[] shopPanel;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButton;


    private void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
            shopPanel[i].SetActive(true);
        kyroUI.text = "Kyro:" + kyro.ToString();
        LoadPanels();
        checkPurchasable();
    }
    

    private void Update()
    {
        
    }


    public void checkPurchasable()
    {
        for (int i= 0; i < shopItems.Length; i++) 
        {
            if (kyro >= shopItems[i].baseCost) ///If you have enough money you can buy it
            {
                purchaseButton[i].interactable = true;

            }

            else 
            {
                purchaseButton[i].interactable = false;
            }
        }
    
    }



    public void purchaseItem(int purchaseButton)
    {
        if (kyro >= shopItems[purchaseButton].baseCost)
        {
            kyro = kyro - shopItems[purchaseButton].baseCost;
            kyroUI.text = "Kyro:" + kyro.ToString();
            checkPurchasable();
            ///Unlock Item

        }
    
    }



    public void AddKyro() 
    {
        kyro++;
        kyroUI.text = "Kyro:" + kyro.ToString();
        checkPurchasable();
    }

    public void LoadPanels() 
    {

        for (int i = 0; i < shopItems.Length; i++) 
        {
            shopPanels[i].titleText.text = shopItems[i].title;
            shopPanels[i].titleDescription.text = shopItems[i].description;
            shopPanels[i].contentt.text = "Kyro:" + shopItems[i].baseCost.ToString();

        }
    
    }


}
