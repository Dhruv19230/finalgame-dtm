using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D _rb; // Varible for Rigidbody

    [SerializeField]private float moveSpeed;//enemyMove Speed

    [SerializeField]private bool 
     _canGoLeft,//Go Left
     _isHimeInCorrectScale,
     _canShoot;//Variable for Enemy Can Shoot
    
    public LayerMask groundLayer;//Get ground Layer
    public Transform leftbottomcheck,
    rightBottomCheck,
    shootPosition;//asign Shoot Position

    public GameObject laserPrefab;// laser prefab

    public float shootSpeed;//Laser Speed

    public LayerMask playerLayer;



    private float nextTime;
    public float timeRange;
    
    
    private void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();//Assign RigidBody
     

    }
    private  void FixedUpdate()
    {
       EnemyMove();//Call this Function;
    }

    private void Update() 
    { 
        ChangeDirection();//Call this Function;Enemy Directon to turn
        WhereIsEnemyGoingDirection();//Call this Function;
        EnemyShoot();//Shoot Function



    }

    private void WhereIsEnemyGoingDirection()
    {
        StillEnemyCurrentScaleCheck();//Call this Function;

        if(_isHimeInCorrectScale)  //Details are StillEnemyrCurrentScaleCheck
        {
         if(!IsRight())
         {
            _canGoLeft = false;
            
         }
       else  if(!IsLeft())
         {
             _canGoLeft = true;
         }

        }
        else
        {
            if(!IsRight())
            { 
                _canGoLeft = true;
            }
          else  if(!IsLeft())
            {
             _canGoLeft = false;
            }
        }
    }

    private void EnemyMove()
    {
        if(IsGround()){
        
        if(_canGoLeft)
        {
            _rb.velocity = new Vector2(moveSpeed,_rb.velocity.y);//enemy Go left
        }
        else
        {
            _rb.velocity = new Vector2(-moveSpeed,_rb.velocity.y);//enemy Go right
        }

        }
        
    }

    private void StillEnemyCurrentScaleCheck()
    {
       //Note 
       /* This Function Get player scale, 
           This Player  has two position leftbottom check rightbottom check,
           When the player's sides switch. These switch  
        */
        if(transform.localScale.x == 1)
        {
          _isHimeInCorrectScale = true;
        }
        else if(transform.localScale.x == -1)
        {
             _isHimeInCorrectScale = false;
        }
    }

    private void ChangeDirection()
    {
        if(_canGoLeft)
        {
            ControlEnemySiding(1);
        }
        else
        {
            ControlEnemySiding(-1);
        }
    }

    private void ControlEnemySiding(int value) //player direction Change
    {
         Vector2 tempScale = transform.localScale;
         tempScale.x = value;
         transform.localScale = tempScale;

    }

private void EnemyShoot()//Enemy Shooting
{
  if(IsPlayerIsNear())
  {
      _canShoot =true;
  }
  else
  {
        _canShoot = false;
  }
  

    if(_canShoot)
    {
        if(Time.time > nextTime)
        {
           nextTime = Time.time + timeRange;
           GameObject laser =  Instantiate(laserPrefab,shootPosition.position,Quaternion.identity);//Duplicate Bullet
           if(_isHimeInCorrectScale)
           {
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed,0);
                FindObjectOfType<AudioManeger>().Play("EnemyLaser");
           }
           else
           {
               laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed,0);
               FindObjectOfType<AudioManeger>().Play("EnemyLaser");
           }
           
        }
    }
  
}

/*This bool methods are checked ground hit or not*/
   private bool IsGround()
   {
      return Physics2D.Raycast(transform.position,Vector2.down,4f,groundLayer);//Check ground
   }

  
  private bool IsRight()
  {
     return Physics2D.Raycast(rightBottomCheck.position,Vector2.down,0.1f,groundLayer);//Check can go Right
  }
  
   private bool IsLeft()
  {
     return Physics2D.Raycast(leftbottomcheck.position,Vector2.down,0.1f,groundLayer);// check can go left
  }

private bool IsPlayerIsNear()

// if Find Player is near
{
    if(_isHimeInCorrectScale)
    {
          return Physics2D.Raycast(transform.position,Vector2.right,5f,playerLayer);
    }
    else
    {
         return Physics2D.Raycast(transform.position,Vector2.left,5f,playerLayer);
    }


}
   
    


  private void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.tag == "Enemy") 
         {
            _rb.gravityScale = 0;
            GetComponent<CapsuleCollider2D>().isTrigger = true;         
         }

         
   }


   private void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.gameObject.tag == "PlayerLaser")//Enemy is hit player Laser Enemy is destroy
         {
            FindObjectOfType<AudioManeger>().Play("EnemyDestroy");
             Destroy(this.gameObject);
         }

         else if(other.gameObject.tag == "DeadLine")
         {
            Destroy(this.gameObject);
         }
   }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy")
         {
            _rb.gravityScale = 1;
            GetComponent<CapsuleCollider2D>().isTrigger = false;           
         }
    }


   
    
}


