using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Kyro_100 : MonoBehaviour
{
  
    public TextMeshProUGUI textCurrency;
    public int defaultCurrency;
    public int currency;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Kyro100")
        {
            currency += 100;
            textCurrency.text = currency.ToString();
            Destroy(other.gameObject);

        }
    }
}
