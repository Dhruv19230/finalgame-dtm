using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
     public float speed;//Variable for Rocket Up Speed;
     public bool canLaunched; // variable for Rocket is Launch or Not

     private void Start() {
          transform.GetChild(0).gameObject.SetActive(false);
     }

     private void Update() 
     {
        if(canLaunched)
        {
            transform.Translate(Vector3.up*speed*Time.deltaTime);
            transform.GetChild(0).gameObject.SetActive(true);
        }
         
     }
   
    private  void OnTriggerEnter2D(Collider2D other) 
    {
          if(other.gameObject.tag == "TopLine")
        {
             FindObjectOfType<LevelManeger>().LevelComplete();// Call Level maneger Level Panel Active
             Destroy(gameObject);
        }
    }
    
   
     
}
