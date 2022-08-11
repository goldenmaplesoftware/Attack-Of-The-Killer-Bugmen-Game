using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_76239mm : MonoBehaviour
{
    public AK47_Advanced ak47;
    [SerializeField] public int bulletInventoryToAddAK;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") 
        {

            kimberCustom.addAmmo();

        }
    }
    */

    public void AddAmmo(int bulletInventoryToAddAK)
    {
        ak47.addAmmo();
    }
}
