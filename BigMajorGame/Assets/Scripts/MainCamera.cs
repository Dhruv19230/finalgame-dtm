using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public bool canFollow; //give permission go with player

    Vector3 
    offset; 

    /*if canfollow is true camera is follow player
    if it is not not follow*/

  private void Start() 
  {
    /*This method get player and camera position and get space both object*/
     offset = transform.position - player.transform.position;   // Get Offset and Casmera
     canFollow = true;
  } 
 
    private void Update()
    {
        GetOffset();
        StaticPos();
    }


    private void GetOffset()
    {
        if(canFollow)
        transform.position = new Vector3(player.transform.position.x + offset.x,transform.position.y,-10);//Camera Go With player
       
    }

    private void StaticPos()
    {
          if(transform.position.x <= 0)// if position = 0  lock camera
          {
             transform.position = new Vector3(0,transform.position.y,transform.position.z);
          }
    }

    
    
       
    
}
