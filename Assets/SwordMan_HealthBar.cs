using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordMan_HealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthbar;
    float health, maxHealth = 100;
    float lerpSpeed;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthText.text = "Health:" + health + "%";
        if (health > maxHealth)
            health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChange();
    }

    void HealthBarFiller() 
    {
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount,health/maxHealth,lerpSpeed);
    }

    void ColorChange() 
    {
        Color healthColor = Color.Lerp(Color.red, Color.blue, (health / maxHealth));
        healthbar.color = healthColor;
    }

    public void Damage(float damagePoints) 
    {
        if (health > 0)
            health -= damagePoints;
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    
    
    }







}
