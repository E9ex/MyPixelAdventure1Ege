using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int damage = 40;
    
    
    private void Update()
    {
        transform.Translate(Vector3.right*10f*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        EnemyBody enemyBody= col.GetComponent<EnemyBody>();
       
       if (enemyBody!=null )
       {
           Destroy(gameObject);//bullet destroy.
           enemyBody.Takedamage(damage);
          
       }
       else if (col.CompareTag("Bulletdest"))
       {
           Destroy(gameObject);
       }

       
    }
    

   
    
}
