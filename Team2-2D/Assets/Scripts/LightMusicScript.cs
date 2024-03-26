using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMusicScript : MonoBehaviour
{
    AudioSource lightMusic;
    // Start is called before the first frame update
    void Start()
    {
        lightMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        lightMusic.Play();
    }
}
