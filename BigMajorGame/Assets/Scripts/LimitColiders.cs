using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitColiders : MonoBehaviour
{
    

    private  void OnTriggerEnter2D(Collider2D other) 
    {
         if(other.CompareTag("PlayerLaser"))
         {
             Destroy(other.gameObject);//Destroy Bullet 
         }

         else if(other.CompareTag("EnemyLaser"))
         {
             Destroy(other.gameObject);//Destroy Bullet 
         }
    }
}
