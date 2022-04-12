using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Manager : MonoBehaviour
{
    public GameObject humanManager;

    public GameObject RainFall;
    public Light DirectLight;
    public GameObject Audio;
    public AudioClip Sun;
    public AudioClip Rain;
    public int timeSpeed = 1;

    foodBrain[] foodMush;
    poisonBrain[] poisonMush;
    magicBrain[] magicMush;
    humanBrain3[] juniorHuman;
    humanBrain2[] interHuman;
    humanBrain1[] proHuman;


// Start is called before the first frame update
void Start()
    {
        setSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        exit();
    }

    void exit()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(0);
        }
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

    void setSpeed()
    {
        timeSpeed = 1;

        humanManager.GetComponent<humanManager>().timeSpeed = timeSpeed;

        foodMush = FindObjectsOfType<foodBrain>();
        poisonMush = FindObjectsOfType<poisonBrain>();
        magicMush = FindObjectsOfType<magicBrain>();

        for (int i = 0; i < foodMush.Length; i++)
        {
            foodMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < poisonMush.Length; i++)
        {
            poisonMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < magicMush.Length; i++)
        {
            magicMush[i].timeSpeed = timeSpeed;
        };

        juniorHuman = FindObjectsOfType<humanBrain3>();
        interHuman = FindObjectsOfType<humanBrain2>();
        proHuman = FindObjectsOfType<humanBrain1>();

        for (int i = 0; i < juniorHuman.Length; i++)
        {
            juniorHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < interHuman.Length; i++)
        {
            interHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < proHuman.Length; i++)
        {
            proHuman[i].timeSpeed = timeSpeed;
        };
    }

    public void changeToSpeed1()
    {
        timeSpeed = 1;

        humanManager.GetComponent<humanManager>().timeSpeed = timeSpeed;

        foodMush = FindObjectsOfType<foodBrain>();
        poisonMush = FindObjectsOfType<poisonBrain>();
        magicMush = FindObjectsOfType<magicBrain>();

        for (int i = 0 ; i< foodMush.Length ; i++)
        {
            foodMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < poisonMush.Length; i++)
        {
            poisonMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < magicMush.Length; i++)
        {
            magicMush[i].timeSpeed = timeSpeed;
        };

        juniorHuman = FindObjectsOfType<humanBrain3>();
        interHuman = FindObjectsOfType<humanBrain2>();
        proHuman = FindObjectsOfType<humanBrain1>();

        for (int i = 0; i < juniorHuman.Length; i++)
        {
            juniorHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < interHuman.Length; i++)
        {
            interHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < proHuman.Length; i++)
        {
            proHuman[i].timeSpeed = timeSpeed;
        };
    }

    public void changeToSpeed2()
    {
        timeSpeed = 5;

        humanManager.GetComponent<humanManager>().timeSpeed = timeSpeed;


        for (int i = 0; i < foodMush.Length; i++)
        {
            foodMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < poisonMush.Length; i++)
        {
            poisonMush[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < magicMush.Length; i++)
        {
            magicMush[i].timeSpeed = timeSpeed;
        };

        juniorHuman = FindObjectsOfType<humanBrain3>();
        interHuman = FindObjectsOfType<humanBrain2>();
        proHuman = FindObjectsOfType<humanBrain1>();

        for (int i = 0; i < juniorHuman.Length; i++)
        {
            juniorHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < interHuman.Length; i++)
        {
            interHuman[i].timeSpeed = timeSpeed;
        };

        for (int i = 0; i < proHuman.Length; i++)
        {
            proHuman[i].timeSpeed = timeSpeed;
        };
    }
}
