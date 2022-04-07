using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomManager : MonoBehaviour
{
    //Variables
    public int howManyFoodMushrooms = 100;
    public int howManyPoisonMushrooms = 20;
    public int howManyMagicMushrooms = 20;
    public GameObject PoisonMushroomObject;
    public GameObject FoodMushroomObject;
    public GameObject MagicMushroomObject;

    // Start is called before the first frame update
    void Start()
    {
        initiateFoodMushRandom();
        initiateMagicMushRandom();
        initiatePoisonMushRandom();
    }

    // Update is called once per frame
    void initiateFoodMushRandom()
    {
        for (int i = 0; i < howManyFoodMushrooms; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            float randYR = Random.Range(-90.0f, 90.0f);
            Vector3 pos = new Vector3(randX, 0.4f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newFoodMush = Instantiate(FoodMushroomObject, pos, rot);
            newFoodMush.name = "FoodMush" + i;
        }
    }

    void initiateMagicMushRandom()
    {
        for (int i = 0; i < howManyMagicMushrooms; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            float randYR = Random.Range(-90.0f, 90.0f);
            Vector3 pos = new Vector3(randX, 0.4f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newFoodMush = Instantiate(MagicMushroomObject, pos, rot);
            newFoodMush.name = "MagicMush" + i;
        }
    }

    void initiatePoisonMushRandom()
    {
        for (int i = 0; i < howManyMagicMushrooms; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            float randYR = Random.Range(-90.0f, 90.0f);
            Vector3 pos = new Vector3(randX, 0.4f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newPoisonMush = Instantiate(PoisonMushroomObject, pos, rot);
            newPoisonMush.name = "PoisonMush" + i;
        }
    }
}
