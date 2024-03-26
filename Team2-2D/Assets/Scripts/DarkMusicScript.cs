using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMusicScript : MonoBehaviour
{
    AudioSource darkMusic;
    // Start is called before the first frame update
    void Start()
    {
        darkMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        darkMusic.Play();
    }
}
