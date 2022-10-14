using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{

    int currentscene;// will assign currentscene IndexNumber
    
    private void Awake()
    {
          currentscene = SceneManager.GetActiveScene().buildIndex;//Assign Scene number
    }

    public  void GotoMainMenu()
    {
         FindObjectOfType<AudioManeger>().Play("ButtonClick");
        SceneManager.LoadScene(0);
    }

    public void GoToNextScene()
    {
         FindObjectOfType<AudioManeger>().Play("ButtonClick");
         SceneManager.LoadScene(currentscene+1);
    }

    public void Restart()
    {
         FindObjectOfType<AudioManeger>().Play("ButtonClick");
        SceneManager.LoadScene(currentscene);
    }

    public void ApplicationQuit()
    {
        FindObjectOfType<AudioManeger>().Play("ButtonClick");
        Application.Quit();
        
    }
    
}
