using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public Transform MuzzleFlashPrefab;
    void Update()
    {
      
        Destroy(this.gameObject, 0.05f);
    }
}
