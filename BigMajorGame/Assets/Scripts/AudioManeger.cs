using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManeger : MonoBehaviour
{
     public Sounds[] sound; 
     
     AudioManeger instance;
     private void Awake() {
         if(instance == null)
         {
            instance = this;
         }
         else
         {
            Destroy(gameObject);
         }
         DontDestroyOnLoad(gameObject);
     }
    private void Start() 
    {
           foreach(Sounds s in sound)
           {
              s.source = gameObject.AddComponent<AudioSource>();
              s.source.name = s.name;
              s.source.clip = s.clip;
              s.source.volume = s.volume;
              
           }
          
    }


    public void Play(string name)
    {
        Sounds s =  Array.Find(sound,sound => sound.name == name);
        if(s == null)
        return;
        s.source.Play();
          
    }


    public void Stop(string name)
    {
        Sounds s =  Array.Find(sound,sound => sound.name == name);
        if(s == null)
        return;
        s.source.Stop();
          
    }
}
