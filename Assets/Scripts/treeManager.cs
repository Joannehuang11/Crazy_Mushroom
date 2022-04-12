using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public List<GameObject> allTree;
    public List<GameObject> allMush;

    humanBrain1[] juniorHuman;
    humanBrain2[] interHuman;
    humanBrain3[] proHuman;

    public NavMeshSurface surface;

    public int isSun = 1;

    public int newTree = 0;
    public int randTreeType;


    // Start is called before the first frame update
    void Start()
    {
        initiateSiteOnSun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initiateTreeAndMush_Trigger()
    {
        newTree = newTree + 1;
        randTreeType = Random.Range(1, 3);
        if (randTreeType == 1)
        {
            float randX = Random.Range(-20.0f, 2.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree1Object, pos, rot);
            newTree.name = "Tree" + newTree;

            allTree.Add(newTree);

            for (int j = 0; j < howManyFoodMushrooms / 5; j++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush_newTree" + newTree + j;

                allMush.Add(newFoodMush);
            }

            for (int k = 0; k < howManyPoisonMushrooms / 5; k++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
                newPoisonMush.name = "PoisonMush_newTree" + newTree + k;

                allMush.Add(newPoisonMush);
            }

            for (int l = 0; l < howManyMagicMushrooms / 5; l++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
                newMagicMush.name = "MagicMush_newTree" + newTree + l;

                allMush.Add(newMagicMush);
            }
        }
        else if (randTreeType == 2)
        {
            float randX = Random.Range(-20.0f, 2.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree2Object, pos, rot);
            newTree.name = "Tree" + newTree;

            allTree.Add(newTree);

            for (int j = 0; j < howManyFoodMushrooms / 5; j++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush_newTree" + newTree + j;

                allMush.Add(newFoodMush);
            }

            for (int k = 0; k < howManyPoisonMushrooms / 5; k++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
                newPoisonMush.name = "PoisonMush_newTree" + newTree + k;

                allMush.Add(newPoisonMush);
            }

            for (int l = 0; l < howManyMagicMushrooms / 5; l++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
                newMagicMush.name = "MagicMush_newTree" + newTree + l;

                allMush.Add(newMagicMush);
            }
        }
        else if (randTreeType == 3)
        {
            float randX = Random.Range(-20.0f, 2.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree3Object, pos, rot);
            newTree.name = "Tree" + newTree;

            allTree.Add(newTree);

            for (int j = 0; j < howManyFoodMushrooms / 5; j++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush_newTree" + newTree + j;

                allMush.Add(newFoodMush);
            }

            for (int k = 0; k < howManyPoisonMushrooms / 5; k++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
                newPoisonMush.name = "PoisonMush_newTree" + newTree + k;

                allMush.Add(newPoisonMush);
            }

            for (int l = 0; l < howManyMagicMushrooms / 5; l++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = pos + randomPos;

                Quaternion rotMush = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
                newMagicMush.name = "MagicMush_newTree" + newTree + l;

                allMush.Add(newMagicMush);
            }
        }
    }

    public void resetSite()
    {
        if ( isSun == 1 )
        {
            clearOriginal();
            initiateSiteOnSun();
            resetHumanMushList();
        }
        else if ( isSun != 1)
        {
            clearOriginal();
            initiateSiteOnRain();
            resetHumanMushList();
        }
    }

    public void mushOnSunny()
    {
        clearOriginalMush();
        instantiateMushroomsAroundsRuin();
        resetHumanMushList();
        isSun = 1;
    }

    public void mushOnRainy()
    {
        clearOriginalMush();
        instantiateMushroomsAroundTrees();
        resetHumanMushList();
        isSun = 0;
    }

    public void initiateSiteOnSun()
    {
        allRuins = new List<GameObject>();
        allTree = new List<GameObject>();
        allMush = new List<GameObject>();
        initiateTree1();
        initiateTree2();
        initiateTree3();
        initiateRuins();
        instantiateMushroomsAroundsRuin();
        surface.BuildNavMesh();
    }

    public void initiateSiteOnRain()
    {
        allRuins = new List<GameObject>();
        allTree = new List<GameObject>();
        allMush = new List<GameObject>();
        initiateTree1();
        initiateTree2();
        initiateTree3();
        initiateRuins();
        instantiateMushroomsAroundTrees();
        surface.BuildNavMesh();
    }

    void clearOriginal()
    {
        for (int i = 0; i < allRuins.Count; i++)
        {
            allRuins[i].SetActive(false);
        };

        for (int i = 0; i < allTree.Count; i++)
        {
            allTree[i].SetActive(false);
        };

        for (int i = 0; i < allMush.Count; i++)
        {
            allMush[i].SetActive(false);
        };

        Debug.Log("clearOriginal");
    }

    void clearOriginalMush()
    {
        for (int i = 0; i < allMush.Count; i++)
        {
            allMush[i].SetActive(false);
        };
    }

    void resetHumanMushList()
    {
        juniorHuman = FindObjectsOfType<humanBrain1>();
        interHuman = FindObjectsOfType<humanBrain2>();
        proHuman = FindObjectsOfType<humanBrain3>();

        for (int i = 0; i < juniorHuman.Length; i++)
        {
            juniorHuman[i].GetComponent<humanBrain1>().resetMush();
        };

        for (int i = 0; i < interHuman.Length; i++)
        {
            interHuman[i].GetComponent<humanBrain2>().resetMush();
        };

        for (int i = 0; i < proHuman.Length; i++)
        {
            proHuman[i].GetComponent<humanBrain3>().resetMush();
        };
    }

    void initiateRuins()
    {
        for (int i = 0; i < howManyRuins; i++)
        {
            float randX = Random.Range(-15.0f, 15.0f);
            float randZ = Random.Range(-15.0f, 15.0f);
            float randRot = Random.Range(-180.0f, 180.0f);
            Vector3 pos = new Vector3(randX, 0.5f, randZ);
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
            float randX = Random.Range(-20.0f, 2.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree1Object, pos, rot);
            newTree.name = "Tree" + i;

            allTree.Add(newTree);

        }
    }

    void initiateTree2()
    {
        for (int i = 0; i < howManyTrees; i++)
        {
            float randX = Random.Range(-20.0f, 20.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 1.0f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree2Object, pos, rot);
            newTree.name = "Tree" + i;

            allTree.Add(newTree);
        }
    }

    void initiateTree3()
    {
        for (int i = 0; i < howManyTrees; i++)
        {
            float randX = Random.Range(-20.0f, 20.0f);
            float randZ = Random.Range(-20.0f, 20.0f);
            Vector3 pos = new Vector3(randX, 0.75f, randZ);
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, 0);
            GameObject newTree = Instantiate(Tree3Object, pos, rot);
            newTree.name = "Tree" + i;

            allTree.Add(newTree);
        }
    }

    //void instantiateMushroomsAroundRuins()
    //{
    //    for (int i = 0; i < allRuins.Count; i++)
    //    {
    //        for (int j = 0; j < howManyFoodMushrooms; j++)
    //        {
    //            float randRadiusX = Random.Range(-6.0f, 6.0f);
    //            float randRadiusZ = Random.Range(-6.0f, 6.0f);
    //            Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
    //            Vector3 xz = allRuins[i].transform.position + randomPos;

    //            Quaternion rot = Quaternion.identity;
    //            //transform.position = new Vector3(xz.x, .5f, xz.z);
    //            GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
    //            newFoodMush.name = "FoodMush" + i;

    //            allMush.Add(newFoodMush);
    //        }

    //        for (int k = 0; k < howManyPoisonMushrooms; k++)
    //        {
    //            float randRadiusX = Random.Range(-4.0f, 4.0f);
    //            float randRadiusZ = Random.Range(-4.0f, 4.0f);
    //            Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
    //            Vector3 xz = allRuins[i].transform.position + randomPos;

    //            Quaternion rot = Quaternion.identity;
    //            //transform.position = new Vector3(xz.x, .5f, xz.z);
    //            GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
    //            newPoisonMush.name = "PoisonMush" + i;

    //            allMush.Add(newPoisonMush);
    //        }

    //        for (int l = 0; l < howManyMagicMushrooms; l++)
    //        {
    //            float randRadiusX = Random.Range(-4.0f, 4.0f);
    //            float randRadiusZ = Random.Range(-4.0f, 4.0f);
    //            Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
    //            Vector3 xz = allRuins[i].transform.position + randomPos;

    //            Quaternion rot = Quaternion.identity;
    //            //transform.position = new Vector3(xz.x, .5f, xz.z);
    //            GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
    //            newMagicMush.name = "MagicMush" + i;

    //            allMush.Add(newMagicMush);
    //        }
    //    }
    //}

    void instantiateMushroomsAroundsRuin()
    {
        for (int i = 0; i < allRuins.Count; i++)
        {
            for (int j = 0; j < howManyFoodMushrooms; j++)
            {
                float randRadiusX = Random.Range(-4.0f, 4.0f);
                float randRadiusZ = Random.Range(-4.0f, 4.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allRuins[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush" + i;

                allMush.Add(newFoodMush);
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

                allMush.Add(newPoisonMush);
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

                allMush.Add(newMagicMush);
            }
        }
    }

    void instantiateMushroomsAroundTrees()
    {
        for (int i = 0; i < allTree.Count; i++)
        {
            for (int j = 0; j < howManyFoodMushrooms / 5; j++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allTree[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newFoodMush = Instantiate(FoodMushroomObject, xz, rot);
                newFoodMush.name = "FoodMush" + i;

                allMush.Add(newFoodMush);
            }

            for (int k = 0; k < howManyPoisonMushrooms / 5; k++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allTree[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newPoisonMush = Instantiate(PoisonMushroomObject, xz, rot);
                newPoisonMush.name = "PoisonMush" + i;

                allMush.Add(newPoisonMush);
            }

            for (int l = 0; l < howManyMagicMushrooms / 5; l++)
            {
                float randRadiusX = Random.Range(-2.0f, 2.0f);
                float randRadiusZ = Random.Range(-2.0f, 2.0f);
                Vector3 randomPos = new Vector3(randRadiusX, 0, randRadiusZ);
                Vector3 xz = allTree[i].transform.position + randomPos;

                Quaternion rot = Quaternion.identity;
                //transform.position = new Vector3(xz.x, .5f, xz.z);
                GameObject newMagicMush = Instantiate(MagicMushroomObject, xz, rot);
                newMagicMush.name = "MagicMush" + i;

                allMush.Add(newMagicMush);
            }
        }
    }
}
