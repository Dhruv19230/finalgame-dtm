using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManeger : MonoBehaviour
{

   public GameObject  

    pausePanel, //PausePAnel Variable
    levelCompletePanel,//levelCommpletePanel
    gameOverPanel//GameOverPanel
    ;
    public TextMeshProUGUI scoreTextValue,lastScoreShow;// Score Value Show Text
    public TextMeshProUGUI LifeTextValue;//Life Value Show Text

    private  int score;//score
    private int life;//Lfe

    private void Start() 
    {
       scoreTextValue.text = score.ToString();
       lastScoreShow.text = score.ToString();
       life = 3;// life = 3
       LifeTextValue.text = life.ToString();
       Time.timeScale = 1;

       AllPanelDisconnect();

       
    }

    private void Update() {
        
         if(Input.GetKeyDown(KeyCode.Escape))
         {
             if(pausePanel.activeInHierarchy == true)
             {
                 pausePanel.SetActive(false);
                 Cursor.visible = false;
                 Time.timeScale = 1;
             }
             else
             {
                
                    pausePanel.SetActive(true);
                    Cursor.visible = true;
                    
                    Time.timeScale = 0;
             }
         }
    }
     public  void  ScoreUP()
     {
        score++;//Score Increase
        scoreTextValue.text = score.ToString();//Score value convert string
        lastScoreShow.text = score.ToString();
     }

     public void LifeDiscrease()
     {
         if(life < 1)
         {
            gameOverPanel.SetActive(true);//Enable game over Panel
            GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerControl>().canControl = false; 
            FindObjectOfType<AudioManeger>().Play("PanelActiveSfx");
         } 
         else
         {
            life--;//Discrease Life
            LifeTextValue.text = life.ToString();//life convert to string
            
            
         }
     }


     private void AllPanelDisconnect()//All Panels Disble  with Start
     {
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
     }

     public void LevelComplete()
     {
        StartCoroutine(LevelPanelActive());
     }


     public IEnumerator LevelPanelActive()
     {
       yield return new WaitForSeconds(1);
       levelCompletePanel.SetActive(true);
       FindObjectOfType<AudioManeger>().Stop("RocketLaunch");
       FindObjectOfType<AudioManeger>().Play("PanelActiveSfx");
     }

     


}
