using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> mushroomSet;
    Dictionary<int, GameObject> mushroomGroups;
    public GameObject foodMushroom;
    public GameObject poisonMushroom;
    public GameObject invisibleMushroom;

    int map_width = 50;
    int map_height = 50;

    List<List<int>> noiseGrid = new List<List<int>>();
    List<List<GameObject>> mushroomGrid = new List<List<GameObject>>();

    // recommend 4 to 20
    float magnification = 4.0f;

    int xOffset = 0; // <- +>
    int zOffset = 0; // v- +^

    // Start is called before the first frame update
    void Start()
    {
        CreateMushroomSet();
        CreateMushroomGroups();
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMushroomSet()
    {
        /** Collect and assign ID codes to the tile prefabs, for ease of access.
    		Best ordered to match land elevation. **/

        mushroomSet = new Dictionary<int, GameObject>();
        mushroomSet.Add(0, invisibleMushroom);
        mushroomSet.Add(1, invisibleMushroom);
        mushroomSet.Add(2, foodMushroom);
        mushroomSet.Add(3, poisonMushroom);
    }

    void CreateMushroomGroups()
    {
        /** Create empty gameobjects for grouping tiles of the same type, ie
    		forest tiles **/

        mushroomGroups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in mushroomSet)
        {
            GameObject mushroomGroup = new GameObject(prefab_pair.Value.name);
            mushroomGroup.transform.parent = gameObject.transform;
            mushroomGroup.transform.localPosition = new Vector3(0, 0, 0);
            mushroomGroups.Add(prefab_pair.Key, mushroomGroup);
        }
    }

    void GenerateMap()
    {
        /** Generate a 2D grid using the Perlin noise fuction, storing it as
    		both raw ID values and tile gameobjects **/

        for (int x = 0; x < map_width; x++)
        {
            noiseGrid.Add(new List<int>());
            mushroomGrid.Add(new List<GameObject>());

            for (int z = 0; z < map_height; z++)
            {
                int mushroomID = GetIdUsingPerlin(x, z);
                noiseGrid[x].Add(mushroomID);
                CreateMushroom(mushroomID, x, z);
            }
        }
    }

    int GetIdUsingPerlin(int x, int z)
    {
        /** Using a grid coordinate input, generate a Perlin noise value to be
    		converted into a tile ID code. Rescale the normalised Perlin value
    		to the number of tiles available. **/

        float raw_perlin = Mathf.PerlinNoise(
            (x - xOffset) / magnification,
            (z - zOffset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin); // Thanks: youtu.be/qNZ-0-7WuS8&lc=UgyoLWkYZxyp1nNc4f94AaABAg
        float scaled_perlin = clamp_perlin * mushroomSet.Count;

        // Replaced 4 with tileset.Count to make adding tiles easier
        if (scaled_perlin == mushroomSet.Count)
        {
            scaled_perlin = (mushroomSet.Count - 1);
        }
        return Mathf.FloorToInt(scaled_perlin);
    }

    void CreateMushroom(int mushroomID, int x, int z)
    {
        /** Creates a new tile using the type id code, group it with common
    		tiles, set it's position and store the gameobject. **/

        GameObject mushroomPrefab = mushroomSet[mushroomID];
        GameObject mushroomGroup = mushroomGroups[mushroomID];
        GameObject mushroom = Instantiate(mushroomPrefab, mushroomGroup.transform);

        mushroom.name = string.Format("mushroom_x{0}_y{1}", x, z);
        mushroom.transform.localPosition = new Vector3(x, .5f, z);

        mushroomGrid[x].Add(mushroom);
    }
}
