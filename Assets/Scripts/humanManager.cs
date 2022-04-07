using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanManager : MonoBehaviour
{
    //Variables
    public GameObject HumanAObject;
    int howManyInitialHumans = 5;
    public int totalHumans;
    public int count = 0;
    public float minHealth;

    humanBrain2[] allNorHuman;


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
        initiateHumanA_Born();
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
        }
    }

    void initiateHumanA_Born()
    {
        allNorHuman = FindObjectsOfType<humanBrain2>();
        totalHumans = allNorHuman.Length;
        populationString = totalHumans + "";
        textElementPopulation.text = populationString;

        count++;
        if (count > 500)
        {
            for (int i = 0; i < totalHumans; i++)
            {
                if (i == 0)
                {
                    minHealth = allNorHuman[i].humanHealth;
                }
                if (allNorHuman[i].humanHealth < minHealth)
                {
                    minHealth = allNorHuman[i].humanHealth;
                }
            }

            if (minHealth > 30)
            {
                float randX = Random.Range(-24.0f, 24.0f);
                float randZ = Random.Range(-24.0f, 24.0f);
                Vector3 pos = new Vector3(randX, 0.25f, randZ);
                GameObject newHumanA = Instantiate(HumanAObject, pos, Quaternion.identity);
                newHumanA.name = "Human_A_new";
                newHumanA.gameObject.tag = "HumanA";
                count = 0;
            }

        }
    }

}
