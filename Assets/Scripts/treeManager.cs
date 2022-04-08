using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeManager : MonoBehaviour
{
    public int howManyTrees = 10;
    public int howManyRuins = 3;
    public GameObject RuinsObject;
    public GameObject Tree1Object;
    public GameObject Tree2Object;
    public GameObject Tree3Object;

    public int howManyFoodMushrooms = 25;
    public int howManyPoisonMushrooms = 5;
    public int howManyMagicMushrooms = 5;
    public GameObject PoisonMushroomObject;
    public GameObject FoodMushroomObject;
    public GameObject MagicMushroomObject;

    public List<GameObject> allRuins;

    // Start is called before the first frame update
    void Start()
    {
        allRuins = new List<GameObject>();

        initiateTree1();
        initiateTree2();
        initiateTree3();
        initiateRuins();
        instantiateMushroomsAroundRuins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initiateRuins()
    {
        for (int i = 0; i < howManyRuins; i++)
        {
            float randX = Random.Range(-15.0f, 15.0f);
            float randZ = Random.Range(-15.0f, 15.0f);
            float randRot = Random.Range(-180.0f, 180.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, randRot, 0);
            GameObject newRuin = Instantiate(RuinsObject, pos, rot);
            newRuin.name = "Ruins" + i;

            allRuins.Add(newRuin);
        }
    }

    void initiateTree1()
    {
        for (int i = 0; i < howManyTrees; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree1Object, pos, rot);
            newTree.name = "Tree" + i;
        }
    }

    void initiateTree2()
    {
        for (int i = 0; i < howManyTrees; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree2Object, pos, rot);
            newTree.name = "Tree" + i;
        }
    }

    void initiateTree3()
    {
        for (int i = 0; i < howManyTrees; i++)
        {
            float randX = Random.Range(-24.0f, 24.0f);
            float randZ = Random.Range(-24.0f, 24.0f);
            Vector3 pos = new Vector3(randX, 0.75f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree3Object, pos, rot);
            newTree.name = "Tree" + i;
        }
    }

    void instantiateMushroomsAroundRuins()
    {
        for (int i = 0; i < allRuins.Count; i++)
        {
            for (int j = 0; j < howManyFoodMushrooms; j++)
            {
                float randRadiusX = Random.Range(-8.0f, 8.0f);
                float randRadiusZ = Random.Range(-8.0f, 8.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allRuins[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush" + i;
            }

            for (int k = 0; k < howManyPoisonMushrooms; k++)
            {
                float randRadiusX = Random.Range(-4.0f, 4.0f);
                float randRadiusZ = Random.Range(-4.0f, 4.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allRuins[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
                newPoisonMush.name = "PoisonMush" + i;
            }

            for (int l = 0; l < howManyMagicMushrooms; l++)
            {
                float randRadiusX = Random.Range(-4.0f, 4.0f);
                float randRadiusZ = Random.Range(-4.0f, 4.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allRuins[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
                newMagicMush.name = "MagicMush" + i;
            }
        }
    }
}
