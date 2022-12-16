using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0f;
    public float tileLength = 30f;
    public int numberOfTiles = 5;
    public Transform playerTransform;
   
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<numberOfTiles; i++)
        {
            if(i == 0)
             SpawnTile(0);
             else
                 SpawnTile(Random.Range(0,tilePrefabs.Length));
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 35 > - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
          
        }
        
    }
    public void SpawnTile(int tileIndex)
    {
       Instantiate(tilePrefabs[tileIndex],transform.forward * zSpawn,transform.rotation);
     
        zSpawn += tileLength;
    }
  
}
