using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject darkMode, lightMode, killerGohst;
    [SerializeField] private KeyCode activatedarkToggleKey = KeyCode.P;
    [SerializeField] private KeyCode deactivatedarkToggleKey = KeyCode.O;
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip switchSFX;

    //JAY PUT THIS HERE
    public GameObject darkBackground;
    public GameObject lightBackground;

    // Start is called before the first frame update
    void Start()
    {
        DisableEnableDarkModeGohst();
        darkMode.SetActive(false);
        lightMode.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activatedarkToggleKey))
        {
            audioSource.clip = switchSFX;
            audioSource.Play();
            Invoke("EnableDarkModeGohst", 1f);
            darkMode.SetActive(true);
            lightMode.SetActive(false);
            animator.SetBool("GohstNoMore", false);

            //JAY PUT THIS IN HERE
            darkBackground.SetActive(true);
            lightBackground.SetActive(false);

        }
        if (Input.GetKeyDown(deactivatedarkToggleKey))
        {
            audioSource.clip = switchSFX;
            audioSource.Play();
            Invoke("DisableEnableDarkModeGohst", 1f);
            darkMode.SetActive(false);
            lightMode.SetActive(true);
            animator.SetBool("GohstNoMore", true);

            //JAY PUT THIS HERE
            darkBackground.SetActive(false);
            lightBackground.SetActive(true);
        }

    }
    void EnableDarkModeGohst()
    {
        killerGohst.SetActive(true);
    }
    void DisableEnableDarkModeGohst()
    {

        killerGohst.SetActive(false);
    }


}
   

