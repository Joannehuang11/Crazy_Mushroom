using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject RainFall;
    public Light DirectLight;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeToRain()
    {
        RainFall.SetActive(true); 
        DirectLight.intensity = 0.5f;
    }

    public void changeToSun()
    {
        RainFall.SetActive(false);
        DirectLight.intensity = 1.0f;

    }
}
