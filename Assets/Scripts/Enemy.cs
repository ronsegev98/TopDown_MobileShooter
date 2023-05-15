using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health,maxHealth = 3f;
    [SerializeField] private float speed;
    private bool canTakeDamage = true;
    private Transform playerTransform;
    private Transform enemyPos;
    public float collisionRadius = 1f;

    private void Start()
    {
        health = maxHealth;
        // Find the player object using its tag and get its transform component
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        // Calculate the direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(direction);
        
    }

    private void OnCollisionStay(Collision collision)  
    {
        
        if(collision.gameObject.TryGetComponent<Player>(out Player playerHealth))
        {
           if (canTakeDamage)
            {
              StartCoroutine (WaitForSeconds());
              playerHealth.TakeDamage(1);
            }
        } 
    }

    public void TakeDamage(float damageamount)
    {

        health -= damageamount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }
    //for continous damage with a delay
    IEnumerator WaitForSeconds()
    {
        canTakeDamage = false;
        yield return new WaitForSecondsRealtime (2);
        canTakeDamage = true;
    }
}