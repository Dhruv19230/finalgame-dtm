using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
     Rigidbody2D _rb; //Rigidbody Variable
     RaycastHit2D _hit;

     private Animator anim;// animator variable

     
     [SerializeField]private float moveSpeed;//Player Move Speed;

     private float horizontalInput;// Variable of Get Horizontal Inputs 
     public float bulletSpeed;//variable of bullet speed

     private float nextFire;
     [SerializeField]private float timeRange;//Time gap

     private bool 
     _isGoLeft,//Can Go Left
     _isGoright,//Can Go Right
     _canJump,// CanJump
     _canFire// CanFire Variable
     ;
     
     private bool turnRight;// if player is turned Right

     public bool canControl; // Lock Control
     [SerializeField]private int _jumpForce;
     
     public LayerMask groundLayer;
     public Transform shootpostion; // varible for shoot position transform;
     public GameObject laserPrefab;

     private void Start() 
     {
       
       _rb = GetComponent<Rigidbody2D>();//Get Player Rigidbody
       anim = GetComponent<Animator>();

       turnRight = true; //laser firstly go right
       canControl = true;

     }

     private void FixedUpdate() 
     {
       if(canControl){
        Moving();//Call Moveing Function
       }
       
     }

     private void Update()
     {
      
        GetKeyBoardInputAndControls();//Cal function
       
       if(canControl){
        Shoot();
       }
      
        
     } 

     private void GetKeyBoardInputAndControls()//Assign Keyboard Inputs
     {
          horizontalInput = Input.GetAxis("Horizontal");// Get WASD or ArrowKeys Inputs

          if(horizontalInput > 0)
          {
            _isGoright = true;//can go right true
            PlayerFilip(1);//player turn right
            turnRight = true;
            anim.SetBool("isWalk",true);
          }
          else if(horizontalInput < 0)
          {
            _isGoLeft =true;// can go left true
            PlayerFilip(-1);//player turn left
            turnRight = false;
            anim.SetBool("isWalk",true);
          }
          else
          {
            _isGoright = false;  //Not Move
            _isGoLeft = false;
            anim.SetBool("isWalk",false);
          }

         if(Input.GetKeyDown(KeyCode.Space))// press Space
         {
            if(IsGround())//If player is Ground
            {
               _canJump = true;               
            }
           
         }

         if(Input.GetMouseButton(0)) //Press Mouse Button
         {
            _canFire = true;// Can Fire
            
             
         }
         else if(Input.GetMouseButtonUp(0))//Mouse Button Up
         {
            _canFire = false;
         }
     }

     private void Moving()//Player Move according to the player Input
     {
        if(_isGoLeft)
        {
          if(IsGround()){
            _rb.velocity = Vector2.left*moveSpeed;//Go Left
          }
        }
        else if(_isGoright)
        {
          if(IsGround()){
            _rb.velocity = Vector2.right*moveSpeed;//Go Right
          }
        }
        else
        {
            if(IsGround())
            {
               _rb.velocity = Vector2.zero;// Stop moving
            }
            else
            {
               _rb.velocity = new Vector2(_rb.velocity.x,_rb.velocity.y);
            }                 
        }

        if(_canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x,_jumpForce);//Jump force
            _canJump = false;
        }
     }

     private void Shoot()
     {
         if(_canFire)
         {
             if(Time.time > nextFire)//Assign Time for laser fire time gap
             {
                 nextFire = Time.time + timeRange;
                  GameObject Laser = Instantiate(laserPrefab,shootpostion.position,Quaternion.identity);
                  FindObjectOfType<AudioManeger>().Play("LaserBigShot");

                  if(turnRight){
                     Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed,0);//bullet Go Right
                  }
                  else
                  {
                     Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed,0);//bullet Go Left
                  }
             }
            
         }
     }

  
    

     private bool IsGround()// Check bool for player is Ground
     {
        return Physics2D.Raycast(transform.position,Vector2.down,1,groundLayer);     
     }


     private void PlayerFilip(int value)//Player facing Side Configure
     {
         Vector2 localscale = transform.localScale; //Assign local Scale temporyVariable
         localscale.x = value; // Change ScaleX Value
         transform.localScale = localscale; // return changes value to local Scale
     }


     private void OnTriggerEnter2D(Collider2D other) 
     {
        if(other.CompareTag("Collectable"))
        {
            FindObjectOfType<AudioManeger>().Play("CollectSound");
            FindObjectOfType<LevelManeger>().ScoreUP();
            Destroy(other.gameObject);

        }
        else if(other.CompareTag("EnemyLaser"))
        {
            FindObjectOfType<LevelManeger>().LifeDiscrease();
        }

      

       else if(other.gameObject.tag == "Rocket")
         {
            FindObjectOfType<MainCamera>().canFollow = false;//Disble camera Follow
            other.gameObject.GetComponent<Rocket>().canLaunched =true;//Enable Rocket Launch
            GetComponent<SpriteRenderer>().enabled = false;//Player Hide
            canControl = false;//Disble Control
            FindObjectOfType<AudioManeger>().Play("RocketLaunch");//call this Audio
                
         }
         else if(other.gameObject.tag == "DeadLine")
         {
            FindObjectOfType<MainCamera>().canFollow = false;//Disble camera Follow
            GetComponent<SpriteRenderer>().enabled = false;//Player Hide
            canControl = false;//Disble Control
            FindObjectOfType<LevelManeger>().gameOverPanel.SetActive(true);
            FindObjectOfType<AudioManeger>().Play("PanelActiveSfx");
         }
     }

     private void OnCollisionEnter2D(Collision2D other) 
     {
         if(other.gameObject.tag == "Enemy")
         {
              FindObjectOfType<LevelManeger>().LifeDiscrease();
         } 
     }


     

     




     
}
