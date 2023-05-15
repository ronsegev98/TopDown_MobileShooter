using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] Transform leftfirePoint;
    [SerializeField] Transform rightfirePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float firingSpeed;
    [SerializeField] float bulletforce;
    

    public static shooting Instance;

    private float lastTimeShot = 0;


    void Awake()
    {
        Instance = GetComponent<shooting>();
        
    }

    public void Shoot()
    {
        if(lastTimeShot + firingSpeed < Time.time)
        {
            lastTimeShot = Time.time;
            GameObject bulletleft = Instantiate(bulletPrefab,leftfirePoint.position,leftfirePoint.rotation);
            GameObject bulletright = Instantiate(bulletPrefab,rightfirePoint.position,rightfirePoint.rotation);
            Rigidbody rb1 = bulletleft.GetComponent<Rigidbody>();
            Rigidbody rb2 = bulletright.GetComponent<Rigidbody>();

            rb1.AddForce(leftfirePoint.forward * bulletforce, ForceMode.Impulse);
            rb2.AddForce(rightfirePoint.forward * bulletforce, ForceMode.Impulse);
        }
            
    }
}


