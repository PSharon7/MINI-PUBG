using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerate : MonoBehaviour {

    public GameObject player;

    public int sizeX;
    public int sizeZ;

    public int groundHeight;
    public float terrainDetail;
    public float terrainHeight;
    int seed;

    public GameObject[] blocks;

    public GameObject environmentTransform;

	// Use this for initialization
	void Start () {
        seed = Random.Range(100000, 999999);
        GenerateTerrain();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                int maxY = (int)(Mathf.PerlinNoise((x / 2 + seed) / terrainDetail, (z / 2 + seed) / terrainDetail) * terrainHeight);
                maxY += groundHeight;

                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity) as GameObject;
                grass.transform.SetParent(environmentTransform.transform);

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayer = Random.Range(1, 5);
                    if (y >= maxY - dirtLayer)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(environmentTransform.transform);

                        int chance = Random.Range(0, 100);
                        if (y == maxY - 1 && chance < 3)
                        {
                            CreateTree(new Vector3(x, y + 2, z));
                        }
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(environmentTransform.transform);
                    }

                }

                if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
                }
            }
        }
    }

    public void CreateTree(Vector3 pos)
    {
        GameObject tree = new GameObject();
        tree.transform.SetParent(environmentTransform.transform);
        tree.name = "tree";

        //Logs
        int height = Random.Range(4, 7);
        for (int i = 0; i < height; i++)
        {
            GameObject wood = Instantiate(blocks[3], new Vector3(pos.x, pos.y + i, pos.z), Quaternion.identity);
            wood.transform.SetParent(environmentTransform.transform);
        }

        //Leaves
        float radius = ((float)height / 3) * 2;
        Vector3 center = new Vector3(pos.x, pos.y + height - 1, pos.z);
        for (int i = -(int)radius; i < radius; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                for (int k = -(int)radius; k < radius; k++)
                {
                    Vector3 position = new Vector3(i + center.x, j + center.y, k + center.z);

                    float distance = Vector3.Distance(center, position);
                    if(distance < radius){
                        if (!Physics.CheckBox(position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity))
                        {
                            GameObject leaf = Instantiate(blocks[4], position, Quaternion.identity);
                            leaf.transform.SetParent(environmentTransform.transform);
                        }
                    }
                }
            }
        }
    }
}
