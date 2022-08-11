using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManage_FP : MonoBehaviour
{
    public static WeaponManage_FP instance;
    int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public GameObject bulletImpact;
    public int currentAmmoACP45 = 10;
    public int currentAmmoAK47 = 5;



    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;

    }

    // Update method switches between guns...
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            ///next weapon
            if (currentWeaponIndex < totalWeapons - 1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];

            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ///previous weapon
            if (currentWeaponIndex > 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
            }

        }



    }
}
