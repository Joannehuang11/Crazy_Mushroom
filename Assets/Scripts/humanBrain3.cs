using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanBrain3 : MonoBehaviour
{
    public NavMeshAgent agent;

    public enum State
    {
        HungrySearchFood,
        MoveToEatFood,
        Wander,
        BoredSearchPoison,
        MoveToKillPoison,
        HighSearchMagic,
        MoveToEatMagic,
        Die,
        CantFindFood,
        CantFindMagic
    }

    public State myState = State.Wander;
    public float humanHealth;
    public float humanIntelligence;

    float randRotation;
    float randTimeToRotate;
    float randSpeed;
    float count = 0;
    int timeToReborn = 2000;
    public int timeSpeed = 1;

    int closetFoodId = -1;
    int closetMagicId = -1;
    int closetPoisonId = -1;

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
        randTimeToRotate = Random.Range(300, 500);
        //randTimeToRotate = Random.Range(50, 500);

        //randomize speed
        randSpeed = Random.Range(0.005f, 0.01f);

        //randomize humanHealth
        humanHealth = Random.Range(70.0f, 90.0f);

        allFoodMush = FindObjectsOfType<foodBrain>();
        //Debug.Log("FindAllFood");

        allPoisonMush = FindObjectsOfType<poisonBrain>();
        //Debug.Log("FindAllPoison");

        allMagicMush = FindObjectsOfType<magicBrain>();
        //Debug.Log("FindAllMagic");

        agent.speed = 2 * timeSpeed;
        agent.SetDestination(RandomNavMeshLocation());
    }

    // Update is called once per frame
    void Update()
    {
        count += 1 * timeSpeed;
        stayWithinBounds();

        if (myState == State.Wander)
        {
            //move(1);
            //turnOverTime(1);
            wander(2);
            consumeEnergy();
            findClosestPoisonMush();

            changeState();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Wander!");
        }
        else if (myState == State.BoredSearchPoison)
        {
            consumeEnergy();
            findClosestPoisonMush();
            changeStateIfFindPoison();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("BoredSearchPoison!");
        }
        else if (myState == State.MoveToKillPoison)
        {
            consumeEnergy();
            moveToClosestPoison(); //move(5)
            killPoisonMush(); //check state

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("MoveToKillPoison");
        }
        else if (myState == State.HungrySearchFood)
        {
            consumeEnergy();
            findClosestFoodMush();
            changeStateIfFindFood();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Hungry!");
        }
        else if (myState == State.MoveToEatFood)
        {
            consumeEnergy();
            moveToClosestFood(); //move(3)
            eatFoodMush(); //check state

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Move to Food!");
        }
        else if (myState == State.HighSearchMagic)
        {
            consumeEnergy();
            findClosestMagicMush();
            changeStateIfFindMagic();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("High!");
        }
        else if (myState == State.MoveToEatMagic)
        {
            consumeEnergy();
            moveToClosestMagic(); //move(2), check state
            eatMagicMush(); //check state

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Move to Magic!");
        }
        else if (myState == State.Die)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            };

            //move(0);
            //turnOverTime(0);
        }
        else if (myState == State.CantFindFood)
        {
            agent.SetDestination(RandomNavMeshLocation());

            wander(10);
            consumeEnergy();

            int countMove = 0;
            countMove++;
            if (countMove > 1000)
            {
                changeState();
                countMove = 0;
            }

            if (humanHealth < 1)
            {
                myState = State.Die;
            }
        }
        else if (myState == State.CantFindMagic)
        {
            agent.SetDestination(RandomNavMeshLocation());

            wander(2);
            spin(20);
            consumeEnergy();

            int countMove = 0;
            countMove++;
            if (countMove > 1000)
            {
                changeState();
                countMove = 0;
            }

            if (humanHealth < 1)
            {
                myState = State.Die;
            }
        }

    }

    //function
    //void OnCollisionEnter(Collision collision)
    //{
    //    randRotation = Random.Range(-60.0f, 60.0f);
    //    transform.Rotate(new Vector3(0, randRotation, 0));
    //    Debug.Log("hit !");

    //}

    public void resetMush()
    {
        allFoodMush = FindObjectsOfType<foodBrain>();
        allPoisonMush = FindObjectsOfType<poisonBrain>();
        allMagicMush = FindObjectsOfType<magicBrain>();
        Debug.Log("resetMush3");
    }

    public void changeState()
    {
        if (humanHealth > 89)
        {
            for (int i = 0; i < allMagicMush.Length; i++)
            {
                if (allMagicMush[i].health == 1)
                {
                    myState = State.HighSearchMagic;
                    for (int j = 0; j < transform.childCount; j++)
                    {
                        transform.GetChild(j).GetComponent<Renderer>().material.color = new Color(219 / 225f, 78 / 225f, 78 / 225f);
                    };
                }
                else
                {
                    myState = State.Wander;
                    for (int j = 0; j < transform.childCount; j++)
                    {
                        transform.GetChild(j).GetComponent<Renderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
                    };
                }
            }
        }
        else if (humanHealth < 50 && humanHealth > 0)
        {
            myState = State.HungrySearchFood;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(255 / 225f, 204 / 225f, 0 / 225f);
            };
        }
        else if (humanHealth > 49 && humanHealth < 90)
        {
            for (int i = 0; i < allPoisonMush.Length; i++)
            {
                if (allPoisonMush[i].health == -20)
                {
                    myState = State.BoredSearchPoison;
                    for (int num = 0; num < transform.childCount; num++)
                    {
                        transform.GetChild(num).GetComponent<Renderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
                    };
                }
                else
                {
                    myState = State.Wander;
                    for (int num = 0; num < transform.childCount; num++)
                    {
                        transform.GetChild(num).GetComponent<Renderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
                    };
                }
            }
        }
        else if (humanHealth < 1)
        {
            myState = State.Die;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            };
            Debug.Log("Die!!");

        }
    }

    void findClosestPoisonMush()
    {
        float closestDist = 100000;
        int closestId = -1;
        for (int i = 0; i < allPoisonMush.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allPoisonMush[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestDist && allPoisonMush[i].health == -20)
            {
                closestDist = dist;
                closestId = i;
            }
        }
        closetPoisonId = closestId;
        //Debug.Log("closetPoisonId = " + closetPoisonId);
    }

    void changeStateIfFindPoison()
    {
        if (closetPoisonId != -1)
        {
            myState = State.MoveToKillPoison;
        }
    }

    void moveToClosestPoison()
    {
        //Vector3 p1 = transform.position;
        //Vector3 p2 = allPoisonMush[closetPoisonId].transform.position;
        //Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

        //transform.LookAt(p2Flat);
        //move(1);
        //Debug.Log("closetPoisonId = " + closetPoisonId);
        //Debug.Log("closetPoisonPosition = " + allPoisonMush[closetPoisonId].transform.position);
        agent.SetDestination(allPoisonMush[closetPoisonId].transform.position);
        agent.speed = 1 * timeSpeed;

        if (humanHealth < 50)
        {
            myState = State.HungrySearchFood;
        }

        if (allPoisonMush[closetPoisonId].health == 0)
        {
            myState = State.CantFindFood;
        }
        //Debug.Log("MoveToClosestMagic");

        //Debug.DrawLine(p1, p2, Color.red);
    }

    void killPoisonMush()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = allPoisonMush[closetPoisonId].transform.position;
        float dist = Vector3.Distance(p1, p2);
        if (dist < 1)
        {
            allPoisonMush[closetPoisonId].health = 0;
            //Debug.Log("killPoisonMush");
            changeState();
        }

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
        //Vector3 p1 = transform.position;
        //Vector3 p2 = allMagicMush[closetMagicId].transform.position;
        //Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

        //transform.LookAt(p2Flat);
        //move(1);

        agent.SetDestination(allMagicMush[closetMagicId].transform.position);
        agent.speed = 1 * timeSpeed;

        spin(20);

        if (humanHealth < 90)
        {
            myState = State.Wander;
        }

        if (allMagicMush[closetMagicId].health == 0)
        {
            myState = State.CantFindMagic;
        }

        //Debug.Log("MoveToClosestMagic");

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

    void changeStateIfFindFood()
    {
        if (closetFoodId != -1)
        {
            myState = State.MoveToEatFood;
        }
        else if (closetFoodId == -1)
        {
            myState = State.CantFindFood;
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
        //Vector3 p1 = transform.position;
        //Vector3 p2 = allFoodMush[closetFoodId].transform.position;
        //Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

        //transform.LookAt(p2Flat);
        //move(5);

        agent.SetDestination(allFoodMush[closetFoodId].transform.position);
        agent.speed = 5 * timeSpeed;

        if (humanHealth < 1)
        {
            myState = State.Die;
        }

        if (allFoodMush[closetFoodId].health == 0)
        {
            myState = State.CantFindFood;
        }

        //Debug.Log("MoveToClosestFood");
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

    void consumeEnergy()
    {
        humanHealth -= 0.05f * timeSpeed;
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

    void wander(int speed)
    {
        agent.speed = speed * timeSpeed;
        int countMove = 0;
        countMove++;

        if (countMove > 500 || agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
            countMove = 0;
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        int walkRadius = 25;
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    //void turnOverTime(float multiplier)
    //{
    //    int countMove = 0;
    //    countMove += timeSpeed*2;
    //    if (countMove * multiplier > randTimeToRotate)
    //    {
    //        //rotate in a random orientation
    //        randRotation = Random.Range(-60.0f, 60.0f);
    //        transform.Rotate(new Vector3(0, randRotation, 0));
    //        // Vector3.forward: global vector
    //        countMove = 0;
    //    }
    //}

    //void move(float multiplier)
    //{
    //    transform.position += (transform.forward * randSpeed * timeSpeed * multiplier);
    //}

    void spin(float rot)
    {
        transform.Rotate(0, rot * count, 0);
    }
}
