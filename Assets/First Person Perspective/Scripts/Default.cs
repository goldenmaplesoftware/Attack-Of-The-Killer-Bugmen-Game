using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Default : MonoBehaviour
{
    public int currentAmmo_45ACP = 0;
    [SerializeField] private Image gunUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ///fire shot
        {
            ///Put knife swipe
        }
    }
}
