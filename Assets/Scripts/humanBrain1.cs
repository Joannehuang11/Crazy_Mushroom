using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class humanBrain1 : MonoBehaviour
{
    public NavMeshAgent agent;

    public enum State
    {
        HungrySearchFoodOrPoison,
        MoveToEatFoodOrPoison,
        Wander,
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

    float closestFoodDist = 100000;
    float closestPoisonDist = 100000;
    int closestFoodId = -1;
    int closestPoisonId = -1;
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
        //reborn();

        if (myState == State.Wander)
        {
            //move(1);
            //turnOverTime(1);
            agent.SetDestination(RandomNavMeshLocation());

            wander(2);
            consumeEnergy();

            changeState();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Wander!");
        }
        else if (myState == State.HungrySearchFoodOrPoison)
        {
            consumeEnergy();

            findClosestFoodOrPoisonMush();

            changeStateIfFindFoodOrPoison();

            if (humanHealth < 1)
            {
                myState = State.Die;
            }

            //Debug.Log("Hungry!");
        }
        else if (myState == State.MoveToEatFoodOrPoison)
        {
            consumeEnergy();
            moveToClosestFoodOrPoison(); //move(3)
            eatFoodOrPoisonMush(); //check state

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
        Debug.Log("resetMush1");
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

        //move(1);
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

    public void changeState()
    {
        if (humanHealth > 89)
        {
            myState = State.HighSearchMagic;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(219 / 225f, 78 / 225f, 78 / 225f);
            };
        }
        else if (humanHealth < 50 && humanHealth > 0)
        {
            myState = State.HungrySearchFoodOrPoison;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(255 / 225f, 204 / 225f, 0 / 225f);
            };
        }
        else if (humanHealth > 49 && humanHealth < 90)
        {
            myState = State.Wander;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(200 / 225f, 200 / 225f, 200 / 225f);
            };
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

    void changeStateIfFindFoodOrPoison()
    {
        if (closestFoodId != -1 || closestPoisonId != -1)
        {
            myState = State.MoveToEatFoodOrPoison;
        }

        else if (closestFoodId != -1 && closestPoisonId != -1)
        {
            myState = State.CantFindFood;
        }
    }

    void eatFoodOrPoisonMush()
    {

        if (closestFoodDist > closestPoisonDist)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allPoisonMush[closestPoisonId].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < 1)
            {
                humanHealth += allPoisonMush[closestPoisonId].health;
                allPoisonMush[closestPoisonId].health = 0;
                changeState();
                //Debug.Log("EatFood");
            }
        }
        else
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allFoodMush[closestFoodId].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < 1)
            {
                humanHealth += allFoodMush[closestFoodId].health;
                allFoodMush[closestFoodId].health = 0;
                changeState();
                //Debug.Log("EatPoison");
            }
        }
    }

    void moveToClosestFoodOrPoison()
    {
        if (closestFoodDist > closestPoisonDist)
        {
            //Vector3 p1 = transform.position;
            //Vector3 p2 = allPoisonMush[closestPoisonId].transform.position;
            //Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

            //transform.LookAt(p2Flat);
            agent.SetDestination(allPoisonMush[closestPoisonId].transform.position);
            agent.speed = 5 * timeSpeed;

            if (allPoisonMush[closestPoisonId].health == 0)
            {
                myState = State.CantFindFood;
            }

            //move(3);
            //Debug.Log("MoveToClosestPoison");
        }
        else
        {
            //Vector3 p1 = transform.position;
            //Vector3 p2 = allFoodMush[closestFoodId].transform.position;
            //Vector3 p2Flat = new Vector3(p2.x, p1.y, p2.z);

            //transform.LookAt(p2Flat);
            agent.SetDestination(allFoodMush[closestFoodId].transform.position);
            agent.speed = 5 * timeSpeed;

            if (allFoodMush[closestFoodId].health == 0)
            {
                myState = State.CantFindFood;
            }

            //move(3);
            //Debug.Log("MoveToClosestFood");
        }


        //Debug.DrawLine(p1, p2, Color.red);
    }

    void findClosestFoodOrPoisonMush()
    {
        for (int i = 0; i < allFoodMush.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allFoodMush[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestFoodDist && allFoodMush[i].health > 0)
            {
                closestFoodDist = dist;
                closestFoodId = i;
            }
        }

        for (int i = 0; i < allPoisonMush.Length; i++)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = allPoisonMush[i].transform.position;
            float dist = Vector3.Distance(p1, p2);
            if (dist < closestPoisonDist && allPoisonMush[i].health < 0)
            {
                closestPoisonDist = dist;
                closestPoisonId = i;
            }
        }
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

        if ( countMove > 500 || agent.remainingDistance <= agent.stoppingDistance)
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
