using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource aSource;
    private bool muted;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        aSource.volume = .5f;
    }
    public void Mute()
    {
        muted = !muted;
        aSource.mute = muted;
    }
}
