using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodBrain : MonoBehaviour
{
    public float health = 10;
    public float timeToReborn = 2000;
    public int count = 0;
    public GameObject FoodMushroomObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        reborn();
        die();
        
    }

    //functions
    void reborn()
    {
        count++;
        if (count > timeToReborn && health == 0)
        {
            health = 10;
            count = 0;
            this.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }

    void die()
    {
        if (health == 0)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
