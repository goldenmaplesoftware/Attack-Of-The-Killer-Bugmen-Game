using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MoneyCollect : MonoBehaviour
{
    [SerializeField] private float startingKyro;
    public float currentKyro
    {

        get;
        private set;
    }

    public TextMeshProUGUI textKyroCount;


    private void Awake()
    {
        currentKyro = startingKyro;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Kyro10") 
        {

            currentKyro += 10;
            textKyroCount.text = currentKyro.ToString();
            Destroy(other.gameObject);

        }

        if (other.transform.tag == "Kyro50")
        {

            currentKyro += 50;
            textKyroCount.text = currentKyro.ToString();
            Destroy(other.gameObject);

        }


        if (other.transform.tag == "Kyro100")
        {

            currentKyro += 100;
            textKyroCount.text = currentKyro.ToString();
            Destroy(other.gameObject);

        }

        if (other.transform.tag == "Kyro500")
        {

            currentKyro += 500;
            textKyroCount.text = currentKyro.ToString();
            Destroy(other.gameObject);

        }

        if (other.transform.tag == "Kyro1000")
        {

            currentKyro += 1000;
            textKyroCount.text = currentKyro.ToString();
            Destroy(other.gameObject);

        }
    }

    

}
