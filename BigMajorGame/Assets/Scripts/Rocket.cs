using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

     //This Script is attached by Rocket

     public float speed;//Variable for Rocket Up Speed;
     public bool canLaunched; // variable for Rocket is Launch or Not

     
    /*This method This Rocket Game first child object "It is Rocket Enegine Fire" is disble */

     private void Start() {
          transform.GetChild(0).gameObject.SetActive(false);
     }

 
 /* This Update -  This Rocket is can't launch  if canlaunch is false 
   If it is true Rocket can Go to up 
 */
     private void Update() 
     {
        if(canLaunched)
        {
            transform.Translate(Vector3.up*speed*Time.deltaTime);
            transform.GetChild(0).gameObject.SetActive(true);
        }
         
     }
   
   /*This is tiggering method  this rocket is go up and hit topline game object call level Maneger level panel and destroy this game object */
    private  void OnTriggerEnter2D(Collider2D other) 
    {
          if(other.gameObject.tag == "TopLine")
        {
             FindObjectOfType<LevelManeger>().LevelComplete();// Call Level maneger Level Panel Active
             Destroy(gameObject);
        }
    }
    
   
     
}
