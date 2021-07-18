using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject chunk;

    public int xSize = 9;

    public int zSize = 9;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        int index = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                var chunk = Instantiate(this.chunk, transform);

                chunk.transform.position = new Vector3(this.chunk.GetComponent<MeshGenerator>().xSize * x, 0, this.chunk.GetComponent<MeshGenerator>().zSize * z);

                chunk.GetComponent<MeshGenerator>().index = index;

                index++;
            }
        }
    }
}
