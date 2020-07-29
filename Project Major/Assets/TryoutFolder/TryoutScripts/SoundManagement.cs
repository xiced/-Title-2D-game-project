using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume = 0.5f;
    private AudioSource source;


    public void SourceControl(AudioSource newSource)
    {
        source.clip = clip;
        source = newSource;
    }

    public void PlayClip()
    {
        source.Play();
        source.volume = volume;
    }
}


public class SoundManagement : MonoBehaviour
{
    [SerializeField] Music[] music;

    private void Start()
    {
        
    }
}
