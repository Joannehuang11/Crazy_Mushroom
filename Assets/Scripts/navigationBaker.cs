using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    // Use this for initialization
    void Start()
    {
        bakeNavMesh();
    }

    void Update()
    {

    }

    public void bakeNavMesh()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
