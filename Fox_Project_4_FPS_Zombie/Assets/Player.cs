using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 50f;
    private float originalHealth;
    public Text healthText;
    private readonly char[] numArray = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    public Color healthWarningColor;
    public Color deathColor;

    // Start is called before the first frame update
    void Start()
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
        // Change health text UI
        healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        originalHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);

        health = int.Parse(healthText.text.Substring(indexOfFirstNumber));

    }

    private void Die()
    {
        // Make text read bc player is dead
        healthText.color = deathColor;
        print("You died! :(");
    }

    public void TakeDamage(float damageToTake)
    {
        print("Taking damage of " + damageToTake);
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
        health = int.Parse(healthText.text.Substring(indexOfFirstNumber));
        health -= damageToTake;


        if (health <= 0)
        {
            health = 0;
            // Change health text UI
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
            Die();
        }
        else if(health <= originalHealth * .5)
        {
            // Make text orange to warn player of low health
            healthText.color = healthWarningColor;
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        }
        else
        {
            // Change health text UI
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        }
    }
}
