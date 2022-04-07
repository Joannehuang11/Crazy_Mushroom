using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonBrain : MonoBehaviour
{
    public float health = -50;
    public GameObject PoisonMushroomObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        die();
    }

    void die()
    {
        if (health == 0)
        {
            PoisonMushroomObject.SetActive(false);
        }
    }
}
