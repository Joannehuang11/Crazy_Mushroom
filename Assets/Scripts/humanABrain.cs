using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanABrain : MonoBehaviour
{
    public enum State
    {
        Forage,
        LookForFood,
        EatFood,
        LookForMagic,
        EatMagic,
        High
    };

    public State myState = State.Forage;

    int count = 0;
    float turnMoment = 1000f;
    public float speed = 0.001f;
    public float humanHealth = 50;
    public GameObject humanAPrefab;

    //initiate an empty list
    foodBrain[] allFoodMushrooms;
    poisonBrain[] allPoisonMushrooms;
    magicBrain[] allMagicMushrooms;

    int closestFood = -1;
    int closestMagic = -1;

    // Start is called before the first frame update
    void Start()
    {
        float randomRotationModifier = Random.Range(-180f, 180f);
        transform.Rotate(0f, randomRotationModifier, 0f);

        turnMoment = Random.Range(250f, 2500f);

        //add elements to list
        allFoodMushrooms = FindObjectsOfType<foodBrain>();
        Debug.Log("CHECK");

        allPoisonMushrooms = FindObjectsOfType<poisonBrain>();
        Debug.Log("CHECK");

        allMagicMushrooms = FindObjectsOfType<magicBrain>();
        Debug.Log("CHECK");

    }

    // Update is called once per frame
    void Update()
    {
        //turnOverTime();
        //move();
        //stayWithinBounds();
        //eatFood();
        //eatPoison();
        //eatMagic();
        //die();


        if (myState == State.Forage)
        {
            turnOverTime();
            move(1);
            stayWithinBounds();
            eatFood();
            //eatPoison();
            //eatMagic();
            //changeStateWhenHigh();
            //changeStateWhenSober();
            changeStateWhenHungry();
            //changeStateWhenBored();
            die(); 
            consumeEnergy();
        }
        else if (myState == State.LookForFood)
        {
            findClosestFood();
            changeStateWhenFoodFound();
            consumeEnergy();
            die();
        }
        else if (myState == State.EatFood)
        {
            moveTowardsClosestFood();
            eatFood();
            changeStateWhenSatiated();
            consumeEnergy();
            die();
        }
        else if (myState == State.LookForMagic)
        {
            findClosestMagic();
            changeStateWhenMagicFound();
            consumeEnergy();
            die();
        }
        else if (myState == State.EatMagic)
        {
            moveTowardsClosestMagic();
            eatMagic();
            changeStateWhenHigh();
            consumeEnergy();
            die();
        }
        else if (myState == State.High)
        {
            move(5);
            franticTurn();
            changeStateWhenSober();
            consumeEnergy();
            die();
        }
    }

    void stayWithinBounds()
    {
        //&& = AND
        //|| = OR
        if (transform.position.x > 49 || transform.position.x < 0)
        {
            transform.Rotate(0, 135, 0);
        }

        if (transform.position.z > 49 || transform.position.z < 0)
        {
            transform.Rotate(0, 135, 0);
        }
    }

    void die()
    {
        if (humanHealth < 1)
        {
            Destroy(this.gameObject);
        }
    }

    void move(float multiplier)
    {
        transform.position += (transform.forward * 0.05f);
    }

    void turnOverTime()
    {
        count++;
        if (count > turnMoment)
        {   //rotate in a random orientation

            float randomRotationModifier = Random.Range(-180f, 180f);
            transform.Rotate(0, randomRotationModifier, 0);

            turnMoment = Random.Range(250f, 2500f);
            count = 0;
        }
    }

    void franticTurn()
    {
        count++;
        if (count > turnMoment)
        {   //rotate in a random orientation

            float randomRotationModifier = Random.Range(-180f, 180f);
            transform.Rotate(0, randomRotationModifier, 0);

            turnMoment = Random.Range(50f, 100f);
            count = 0;
        }
    }

    void eatFood()
    {
        for (int i = 0; i < allFoodMushrooms.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allFoodMushrooms[i].transform.position;
            float dist = Vector3.Distance(p1, p2);

            if (dist < 1)
            {
                humanHealth += allFoodMushrooms[i].health;
                allFoodMushrooms[i].health = 0;
                break;
            }
        }
    }

    void eatPoison()
    {
        for (int i = 0; i < allPoisonMushrooms.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allPoisonMushrooms[i].transform.position;
            float dist = Vector3.Distance(p1, p2);

            if (dist < 1)
            {
                humanHealth += allPoisonMushrooms[i].health;
                allPoisonMushrooms[i].health = 0;
                break;
            }
        }
    }
    void eatMagic()
    {
        for (int i = 0; i < allMagicMushrooms.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allMagicMushrooms[i].transform.position;
            float dist = Vector3.Distance(p1, p2);

            if (dist < 1)
            {
                humanHealth += allMagicMushrooms[i].health;
                allMagicMushrooms[i].health = 0;
                break;
            }
        }
    }
    void changeStateWhenHungry()
    {
        if (humanHealth < 30)
        {
            myState = State.EatFood;
        }
    }

    void findClosestFood()
    {
        float closestDist = 10000000;
        int closestId = -1;
        for (int i = 0; i < allFoodMushrooms.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allFoodMushrooms[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestId = i;
            }
        }
        closestFood = closestId;
    }
    void changeStateWhenSatiated()
    {
        if (humanHealth > 49)
        {
            myState = State.Forage;
        }
    }
    void moveTowardsClosestFood()
    {
        Debug.Log("CURRENT CLOSEST FOOD: " + closestFood);
        if (closestFood == -1) findClosestFood();

         Vector3 p1 = transform.position;
        Vector3 p2 = allFoodMushrooms[closestFood].transform.position;
        Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);
        Debug.DrawLine(p1, p2, Color.red);

        transform.LookAt(p2Flat); //rotate - orient 
        move(4);
    }
    void changeStateWhenBored()
    {
        if (humanHealth > 59)
        {
            myState = State.EatMagic;
        }
    }

    void findClosestMagic()
    {
        float closestDist = 10000000;
        int closestId = -1;
        for (int i = 0; i < allMagicMushrooms.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allMagicMushrooms[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestId = i;
            }
        }
        closestMagic = closestId;
    }

    void moveTowardsClosestMagic()
    {
        Debug.Log("Move Towards Closest Magic");
        Vector3 p1 = transform.position;
        Vector3 p2 = allMagicMushrooms[closestMagic].transform.position;
        Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);
        //Debug.DrawLine(p1, p2, Color.red);

        transform.LookAt(p2Flat); //rotate - orient towards the grass
        move(2);
    }

    void changeStateWhenHigh()
    {
        if (humanHealth > 100)
        {
            myState = State.High;
        }
    }

    void changeStateWhenSober()
    {
        if (humanHealth < 60)
        {
            myState = State.Forage;
        }
    }
    void consumeEnergy()
    {
        humanHealth -= 0.1f;
    }

    void changeStateWhenFoodFound()
    {
        if (closestFood != -1)
        {
            myState = State.EatFood;
        }
    }

    void changeStateWhenMagicFound()
    {
        if (closestMagic != -1)
        {
            myState = State.EatMagic;
        }
    }
}
