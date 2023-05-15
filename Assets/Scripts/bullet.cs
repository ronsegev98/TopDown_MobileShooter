using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    [SerializeField] private float maxProjectileDistance;

    private Vector3 firingPoint;

    void Start(){
        firingPoint = transform.position;
    }

    void Update()
    {
        Destroying();
    }

    void Destroying()
    {
        if(Vector3.Distance(firingPoint,transform.position) > maxProjectileDistance){
             Destroy(this.gameObject);

        }      
    }

    private void OnCollisionEnter(Collision collision) {
        
        
        //enemies take damage
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemycomponent))
        {
            enemycomponent.TakeDamage(1);

        }
        if(collision.gameObject.TryGetComponent<Boss>(out Boss bosscomponent))
        {
            bosscomponent.TakeDamage(1);

        }
        
        Destroy(this.gameObject);
    }



    
}
