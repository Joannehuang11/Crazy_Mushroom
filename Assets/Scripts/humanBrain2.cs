using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanBrain2 : MonoBehaviour
{
    public enum State
    {
        HungrySearchFood,
        MoveToEatFood,
        Wander,
        HighSearchMagic,
        MoveToEatMagic,
    }

    public State myState = State.Wander;
    public float humanHealth;

    float randRotation;
    float randTimeToRotate;
    float randSpeed;
    float count = 0;
    int timeToReborn = 2000;

    int closetFoodId = -1;
    int closetMagicId = -1;

    //initiate an empty list
    foodBrain[] allFoodMush;
    poisonBrain[] allPoisonMush;
    magicBrain[] allMagicMush;

    // Start is called before the first frame update
    void Start()
    {
        // start in a random orientation:
        randRotation = Random.Range(-180.0f, 180.0f);
        transform.Rotate(new Vector3(0, randRotation, 0));

        //randomize how many seteps to take before rotation
        randTimeToRotate = 500;
        //randTimeToRotate = Random.Range(50, 500);

        //randomize speed
        randSpeed = Random.Range(0.005f, 0.01f);

        //randomize humanHealth
        humanHealth = Random.Range(30.0f, 90.0f);

        allFoodMush = FindObjectsOfType<foodBrain>();
        Debug.Log("FindAllFood");

        allPoisonMush = FindObjectsOfType<poisonBrain>();
        Debug.Log("FindAllPoison");

        allMagicMush = FindObjectsOfType<magicBrain>();
        Debug.Log("FindAllMagic");
    }

    // Update is called once per frame
    void Update()
    {
        stayWithinBounds();
        consumeEnergy();
        die();
        reborn();

        if (myState == State.Wander)
        {
            move(1,0);
            turnOverTime(1);

            changeState();

            Debug.Log("Wander!");
        }
        else if (myState == State.HungrySearchFood)
        {
            findClosestFoodMush();

            changeStateIfFindFood();

            Debug.Log("Hungry!");
        }
        else if (myState == State.MoveToEatFood)
        {
            moveToClosestFood(); //move(3)
            eatFoodMush(); //check state

            Debug.Log("Move to Food!");
        }
        else if (myState == State.HighSearchMagic)
        {
            findClosestMagicMush();

            changeStateIfFindMagic();

            Debug.Log("High!");
        }
        else if (myState == State.MoveToEatMagic)
        {
            moveToClosestMagic(); //move(2), check state
            eatMagicMush(); //check state

            Debug.Log("Move to Magic!");
        }

    }

    //function

    void OnCollisionEnter(Collision collision)
    {
        randRotation = Random.Range(-60.0f, 60.0f);
        transform.Rotate(new Vector3(0, randRotation, 0));
    }


    void eatMagicMush()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = allMagicMush[closetMagicId].transform.position;
        float dist = Vector3.Distance(p1, p2);
        if (dist < 1)
        {
            humanHealth += allMagicMush[closetMagicId].health;
            allMagicMush[closetMagicId].health = 0;
            changeState();
        }
    }

    void changeStateIfFindMagic()
    {
        if (closetMagicId != -1)
        {
            myState = State.MoveToEatMagic;
        }
    }

    void moveToClosestMagic()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = allMagicMush[closetMagicId].transform.position;
        Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

        transform.LookAt(p2Flat);
        move(3,20);

        if (humanHealth < 90)
        {
            myState = State.Wander;
        }

        Debug.Log("MoveToClosestMagic");

        //Debug.DrawLine(p1, p2, Color.red);
    }

    void findClosestMagicMush()
    {
        float closestDist = 100000;
        int closestId = -1;
        for (int i = 0; i < allMagicMush.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allMagicMush[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestDist && allMagicMush[i].health > 0)
            {
                closestDist = dist;
                closestId = i;
            }
        }
        closetMagicId = closestId;
    }

    void changeState()
    {
        if (humanHealth > 89)
        {
            myState = State.HighSearchMagic;
            GetComponent<Renderer>().material.color = new Color(219/225f, 78/225f, 78/225f);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(219/225f, 78/225f, 78/225f);
            };
        }
        else if (humanHealth < 30)
        {
            myState = State.HungrySearchFood;
            GetComponent<Renderer>().material.color = new Color(255 / 225f, 204 / 225f, 0 / 225f);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(255 / 225f, 204 / 225f, 0 / 225f);
            };
        }
        else if (humanHealth > 29 && humanHealth < 90)
        {
            myState = State.Wander;
            GetComponent<Renderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
            };
        }
    }

    void changeStateIfFindFood()
    {
        if (closetFoodId != -1)
        {
            myState = State.MoveToEatFood;
        }
    }

    void eatFoodMush()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = allFoodMush[closetFoodId].transform.position;
        float dist = Vector3.Distance(p1, p2);
        if (dist < 1)
        {
            humanHealth += allFoodMush[closetFoodId].health;
            allFoodMush[closetFoodId].health = 0;
            changeState();
        }
    }

    void moveToClosestFood()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = allFoodMush[closetFoodId].transform.position;
        Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);
       
        transform.LookAt(p2Flat);
        move(5,0);

        Debug.Log("MoveToClosestFood");
        //Debug.DrawLine(p1, p2, Color.red);
    }

    void findClosestFoodMush()
    {
        float closestDist = 100000;
        int closestId = -1;
        for (int i = 0; i < allFoodMush.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allFoodMush[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestDist && allFoodMush[i].health > 0)
            {
                closestDist = dist;
                closestId = i;
            }
        }
        closetFoodId = closestId;
    }

    void reborn()
    {
        if (humanHealth < 1)
        {
            count++;
        }

        if (count > timeToReborn && humanHealth < 1)
        {
            humanHealth = 80;
            this.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
            count = 0;
        }

    }

    void die()
    {
        if (humanHealth < 1)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void consumeEnergy()
    {
        humanHealth -= 0.1f;
    }

    void stayWithinBounds()
    {
        // && = AND ; || = OR
        if (transform.position.x > 24 || transform.position.x < -24)
        {
            transform.Rotate(0, 90, 0);
        }

        if (transform.position.z > 24 || transform.position.z < -24)
        {
            transform.Rotate(0, 90, 0);
        }
    }

    void turnOverTime(float multiplier)
    {
        count++;
        if (count * multiplier > randTimeToRotate)
        {
            //rotate in a random orientation
            randRotation = Random.Range(-60.0f, 60.0f);
            transform.Rotate(new Vector3(0, randRotation, 0));
            // Vector3.forward: global vector
            count = 0;
        }
    }

    void move(float multiplier, float rot)
    {
        count++;
        transform.position += (transform.forward * randSpeed * multiplier);
        transform.Rotate(0, rot * count, 0);
    }
}
