using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds 
{
       public string  name;
       public AudioClip clip;

       [Range(0,1)]
       public float volume;
       [Range(0,1)]
       public float  pitch;

       public bool loop;

       public AudioSource source;
}
