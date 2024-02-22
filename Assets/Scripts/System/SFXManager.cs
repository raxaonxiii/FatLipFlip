using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager current = null;
    private AudioSource aSource;
    public AudioClip start, button, deal, flip, match, win;

    private void Start()
    {
        if (current == null)
            current = this;

        aSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        aSource.clip = start;
        aSource.Play();
    }

    public void PlayButton()
    {
        aSource.clip = button;
        aSource.Play();
    }

    public void PlayDeal()
    {
        aSource.clip = deal;
        aSource.Play();
    }

    public void PlayFlip()
    {
        aSource.clip = flip;
        aSource.Play();
    }

    public void PlayMatch()
    {
        aSource.clip = match;
        aSource.Play();
    }
    
    public void PlayWin()
    {
        aSource.clip = win;
        aSource.Play();
    }
}
