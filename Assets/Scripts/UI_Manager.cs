using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject RainFall;
    public Light DirectLight;
    public GameObject Audio;
    public AudioClip Sun;
    public AudioClip Rain;


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
        Audio.GetComponent<AudioSource>().clip = Rain;
        Audio.GetComponent<AudioSource>().Play();
    }
    public void changeToSun()
    {
        RainFall.SetActive(false);
        DirectLight.intensity = 1.0f;
        Audio.GetComponent<AudioSource>().clip = Sun;
        Audio.GetComponent<AudioSource>().Play();
    }
}
