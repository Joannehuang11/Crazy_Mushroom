using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanManager : MonoBehaviour
{
    //Variables
    public GameObject HumanAObject;
    public GameObject HumanDumb;
    int howManyInitialHumans = 5;
    public int totalHumans;
    public int count = 0;
    public float minHealth = 999;

    GameObject[] allNorHuman = new GameObject[] { };
    GameObject[] allDumbHuman = new GameObject[] { };
    GameObject[] allHuman = new GameObject[] { };


    //UI
    public string populationString;
    public Text textElementPopulation;


    // Start is called before the first frame update
    void Start()
    {
        initiateHumanA_Start();
    }

    // Update is called once per frame
    void Update()
    {
        totalHumans = allNorHuman.Length + allDumbHuman.Length;

        printPopulation();

        findMinHealth();
        initiateHuman_Born();

        Debug.Log("allNorHuman.Length =" + allNorHuman.Length);
    }

    void printPopulation()
    {
        populationString = totalHumans + "";
        textElementPopulation.text = populationString;
    }

    void initiateHumanA_Start()
    {
        for (int i = 0; i < howManyInitialHumans; i = i + 1)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            Vector3 pos = new Vector3(randX, 0.25f, randZ);
            GameObject newHumanA = Instantiate(HumanAObject, pos, Quaternion.identity);
            newHumanA.name = "Human_A";
            newHumanA.gameObject.tag = "HumanA";
            //newHumanA.gameObject.tag = "Human";
            allNorHuman = GameObject.FindGameObjectsWithTag("HumanA");
        }
    }

    void findMinHealth()
    {
        minHealth = 999;
        for (int i = 0; i < allNorHuman.Length; i++)
        {
            if (allNorHuman[i].GetComponent<humanBrain2>().humanHealth < minHealth)
            {
                minHealth = allNorHuman[i].GetComponent<humanBrain2>().humanHealth;
            }
        }

        for (int i = 0; i < allDumbHuman.Length; i++)
        {
            Debug.Log("Script = " + allDumbHuman[i].GetComponent<humanBrainDumb>());

            if (allDumbHuman[i].GetComponent<humanBrainDumb>().humanHealth < minHealth)
            {
                minHealth = allDumbHuman[i].GetComponent<humanBrainDumb>().humanHealth;
            }
        }

    }

    void initiateHuman_Born()
    {
        count++;
        if (count > 500)
        {

            if (minHealth > 30 & totalHumans < 10)
            {
                float randX = Random.Range(-24.0f, 24.0f);
                float randZ = Random.Range(-24.0f, 24.0f);
                Vector3 pos = new Vector3(randX, 0.25f, randZ);
                GameObject newHumanA = Instantiate(HumanAObject, pos, Quaternion.identity);
                newHumanA.name = "Human_A_new";
                newHumanA.gameObject.tag = "HumanA";
                //newHumanA.gameObject.tag = "Human";
                count = 0;
                allNorHuman = GameObject.FindGameObjectsWithTag("HumanA");
            }


            if (minHealth > 30 && totalHumans < 20 && totalHumans > 9)
            {
                if (totalHumans % 2 == 0)
                {
                    float randX = Random.Range(-24.0f, 24.0f);
                    float randZ = Random.Range(-24.0f, 24.0f);
                    Vector3 pos = new Vector3(randX, 0.25f, randZ);
                    GameObject newHumanDumb = Instantiate(HumanDumb, pos, Quaternion.identity);
                    newHumanDumb.name = "Human_Dumb_new";
                    newHumanDumb.gameObject.tag = "HumanDumb";
                    //newHumanDumb.gameObject.tag = "Human";
                    count = 0;
                    allDumbHuman = GameObject.FindGameObjectsWithTag("HumanDumb");
                }

                if (totalHumans % 2 != 0)
                {
                    float randX = Random.Range(-24.0f, 24.0f);
                    float randZ = Random.Range(-24.0f, 24.0f);
                    Vector3 pos = new Vector3(randX, 0.25f, randZ);
                    GameObject newHumanA = Instantiate(HumanAObject, pos, Quaternion.identity);
                    newHumanA.name = "Human_A_new";
                    newHumanA.gameObject.tag = "HumanA";
                    //newHumanA.gameObject.tag = "Human";
                    count = 0;
                    allNorHuman = GameObject.FindGameObjectsWithTag("HumanA");
                }
            }
        }
    }
}


