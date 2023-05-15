using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameOver gameOver;
    public int maxHealth = 4;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 0)
        {
            gameOver.EndGame();
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage){

        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}

