using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 50f;
    private float originalHealth;
    public Text healthText;
    public Text orbText;
    public Text gunText;
    private readonly char[] numArray = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    public Color healthWarningColor;
    public Color deathColor;
    public Color normalHealthColor;
    public float bandAidHealthBoost = 30f;
    private int goldenOrbs = 0;
    private string[] gun = { "Pistol", "Rifle" };
    private string currentGun;
    public GameObject pistol;
    public GameObject rifle;

    // Start is called before the first frame update
    void Start()
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
        // Change health text UI
        healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;
        originalHealth = health;
        //currentGun = "Pistol";
        ChangeGun("Pistol");
    }

    // Update is called once per frame
    void Update()
    {
        int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);

        health = int.Parse(healthText.text.Substring(indexOfFirstNumber));
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bandaid
        if (other.gameObject.tag.Equals("Bandaid"))
        {
            // Increase health but make sure health doesn't go over original max health
            health = Mathf.Min(health + bandAidHealthBoost, originalHealth);

            int indexOfFirstNumber = healthText.text.IndexOfAny(numArray);
            healthText.text = healthText.text.Substring(0, indexOfFirstNumber) + health;

            if (health <= originalHealth * .5)
            {
                // Make text orange to warn player of low health
                healthText.color = healthWarningColor;
            }
            else
            {
                healthText.color = normalHealthColor;
            }

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Golden Orb"))
        {
            goldenOrbs += 1;
            orbText.text = "Golden Orbs: " + goldenOrbs + "/5";

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Rifle"))
        {
            // TODO: Change gun text
            // First, get index of first spae
            // Then write rifle

            currentGun = "Rifle";
            ChangeGun("Rifle");

            Destroy(other.gameObject);
        }
    }
    private void Die()
    {
        // Make text read bc player is dead
        healthText.color = deathColor;
        print("You died! :(");
    }

    // Changes gun
    private void ChangeGun(string gun)
    {
        currentGun = gun;

        if (gun == "Pistol")
        {
            pistol.SetActive(true);
            // Set all other guns as false
            rifle.SetActive(false);
        }
        else if (gun == "Rifle")
        {
            rifle.SetActive(true);
            // Set all other funs as false
            pistol.SetActive(false);
        }
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
            healthText.color = normalHealthColor;
        }
    }
}
