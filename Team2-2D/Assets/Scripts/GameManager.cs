using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject darkMode;
    [SerializeField] private GameObject lightMode;
    [SerializeField] private KeyCode activatedarkToggleKey = KeyCode.P;
    [SerializeField] private KeyCode deactivatedarkToggleKey = KeyCode.O;
    
    
    // Start is called before the first frame update
    void Start()
    {
        darkMode.SetActive(false);
        lightMode.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activatedarkToggleKey))
        {

            Debug.Log("Dark Mode Was Activated");
            darkMode.SetActive(true);
            lightMode.SetActive(false);

        }
        if (Input.GetKeyDown(deactivatedarkToggleKey))
        {

            Debug.Log("Dark Mode Was Activated");
            darkMode.SetActive(false);
            lightMode.SetActive(true);
        }

    }

    
}
   

