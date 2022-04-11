using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class humanManager : MonoBehaviour
{
    //Variables
    public GameObject HumanJunior;
    public GameObject HumanInter;
    public GameObject HumanPro;
    int howManyInitialHumans = 5;
    public int totalHumans;
    public int count = 0;
    int countDay = 0;
    public float minHealth = 999;
    public int timeSpeed = 1;

    float humanIQ;

    List<GameObject> allHumans_list;
    List<GameObject> junior_list;
    List<GameObject> intermediate_list;
    List<GameObject> pro_list;

    int numJun = 1;
    int numInter = 1;
    int numPro = 1;


    //UI
    public string populationString;
    public Text textElementPopulation;
    public string JuniorNumString;
    public Text textElementJuniorNum;
    public string InterNumString;
    public Text textElementInterNum;
    public string ProNumString;
    public Text textElementProNum;
    public string DayString;
    public Text textElementDayNum;


    // Start is called before the first frame update
    void Start()
    {
        initiateHuman_Start();
        timeSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        count += 1 * timeSpeed;
        countDay += 1 * timeSpeed;

        printPopulation();
        printJuniorNum();
        printIntermediateNum();
        printProNum();
        printDayNum();

        findMinHealth();
        initiateHuman_Born();

        dieRemoveFromList();
    }

    void printPopulation()
    {
        populationString = totalHumans + "";
        textElementPopulation.text = populationString;
    }

    void printJuniorNum()
    {
        JuniorNumString = junior_list.Count + "";
        textElementJuniorNum.text = JuniorNumString;
    }

    void printIntermediateNum()
    {
        InterNumString = intermediate_list.Count + "";
        textElementInterNum.text = InterNumString;
    }

    void printProNum()
    {
        ProNumString = pro_list.Count + "";
        textElementProNum.text = ProNumString;
    }

    void printDayNum()
    {
        float day = Mathf.Floor(countDay/5000);
        DayString = day + "";
        textElementDayNum.text = DayString;
    }

    void initiateHuman_Start()
    {
        int humanIQ = Random.Range(0, 100);
        junior_list = new List<GameObject>();
        intermediate_list = new List<GameObject>();
        pro_list = new List<GameObject>();


        if (humanIQ > 70)
        {
            initiateHuman_Born_Pro();
            //Debug.Log("Pro");
        }
        if (humanIQ < 30)
        {
            initiateHuman_Born_Junior();
            //Debug.Log("Junior");
        }
        if (humanIQ < 71 && humanIQ > 29)
        {
            initiateHuman_Born_Inter();
            //Debug.Log("Inter");
        }
        totalHumans = junior_list.Count + intermediate_list.Count + pro_list.Count;
    }


    void initiateHuman_Born()
    {
        if (count > 1000)
        {
            humanIQ = Random.Range(0, 100);
            if (humanIQ > 70)
            {
                initiateHuman_Born_Pro();
                //Debug.Log("Pro");
            }
            if (humanIQ < 30)
            {
                initiateHuman_Born_Junior();
                //Debug.Log("Junior");
            }
            if (humanIQ < 71 && humanIQ > 29)
            {
                initiateHuman_Born_Inter();
                //Debug.Log("Inter");
            }
            count = 0;
        }
        totalHumans = junior_list.Count + intermediate_list.Count + pro_list.Count;
    }

    void initiateHuman_Born_Junior()
    {
        float randX = Random.Range(-24.0f, 24.0f);
        float randZ = Random.Range(-24.0f, 24.0f);
        Vector3 pos = new Vector3(randX, 0.25f, randZ);
        GameObject newHumanJunior = Instantiate(HumanJunior, pos, Quaternion.identity);
        newHumanJunior.name = "HumanJunior" + numJun;
        newHumanJunior.GetComponent<humanBrain1>().humanIntelligence = humanIQ;
        newHumanJunior.GetComponent<humanBrain1>().timeSpeed = timeSpeed;
        numJun = numJun + 1;

        junior_list.Add(newHumanJunior);
    }

    void initiateHuman_Born_Inter()
    {
        float randX = Random.Range(-24.0f, 24.0f);
        float randZ = Random.Range(-24.0f, 24.0f);
        Vector3 pos = new Vector3(randX, 0.25f, randZ);
        GameObject newHumanInter = Instantiate(HumanInter, pos, Quaternion.identity);
        newHumanInter.name = "HumanInter" + numInter;
        newHumanInter.GetComponent<humanBrain2>().humanIntelligence = humanIQ;
        newHumanInter.GetComponent<humanBrain2>().timeSpeed = timeSpeed;
        numInter = numInter + 1;

        intermediate_list.Add(newHumanInter);
    }

    void initiateHuman_Born_Pro()
    {
        float randX = Random.Range(-24.0f, 24.0f);
        float randZ = Random.Range(-24.0f, 24.0f);
        Vector3 pos = new Vector3(randX, 0.25f, randZ);
        GameObject newHumanPro = Instantiate(HumanPro, pos, Quaternion.identity);
        newHumanPro.name = "HumanPro" + numPro;
        newHumanPro.GetComponent<humanBrain3>().humanIntelligence = humanIQ;
        newHumanPro.GetComponent<humanBrain3>().timeSpeed = timeSpeed;

        pro_list.Add(newHumanPro);
    }

    void findMinHealth()
    {

        for (int i = 0; i < pro_list.Count; i++)
        {
            if (pro_list[i].GetComponent<humanBrain3>().humanHealth < minHealth)
            {
                minHealth = pro_list[i].GetComponent<humanBrain3>().humanHealth;
            }
        }

        for (int i = 0; i < intermediate_list.Count; i++)
        {
            if (intermediate_list[i].GetComponent<humanBrain2>().humanHealth < minHealth)
            {
                minHealth = intermediate_list[i].GetComponent<humanBrain2>().humanHealth;
            }
        }

        for (int i = 0; i < junior_list.Count; i++)
        {
            if (junior_list[i].GetComponent<humanBrain1>().humanHealth < minHealth)
            {
                minHealth = junior_list[i].GetComponent<humanBrain1>().humanHealth;
            }
        }
    }

    void dieRemoveFromList()
    {
        int dieID_Junior;
        int dieID_Inter;
        int dieID_Pro;

        for (int i = 0; i < pro_list.Count; i++)
        {
            if (pro_list[i].GetComponent<humanBrain3>().myState == humanBrain3.State.Die)
            {
                dieID_Pro = i;
                pro_list.RemoveAt(dieID_Pro);
                Debug.Log("remove pro" + dieID_Pro);
                totalHumans = junior_list.Count + intermediate_list.Count + pro_list.Count;
            }
        }

        for (int i = 0; i < intermediate_list.Count; i++)
        {
            if (intermediate_list[i].GetComponent<humanBrain2>().myState == humanBrain2.State.Die)
            {
                dieID_Inter = i;
                intermediate_list.RemoveAt(dieID_Inter);
                Debug.Log("remove inter" + dieID_Inter);
                totalHumans = junior_list.Count + intermediate_list.Count + pro_list.Count;
            }
        }

        for (int i = 0; i < junior_list.Count; i++)
        {
            if (junior_list[i].GetComponent<humanBrain1>().myState == humanBrain1.State.Die)
            {
                dieID_Junior = i;
                junior_list.RemoveAt(dieID_Junior);
                Debug.Log("remove junior" + dieID_Junior);
                totalHumans = junior_list.Count + intermediate_list.Count + pro_list.Count;
            }
        }
    }
}

