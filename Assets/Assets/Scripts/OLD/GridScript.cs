using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridScript : MonoBehaviour{
    public GameObject blockForSpawn;
    public GameObject objectForSpawn;
    public GameObject player;
    public int sizeX = 50;
    public int sizeZ = 50;
    private int noiseHeight = 10;
    private Vector3 startPosition;
    private Hashtable blockContainer = new Hashtable();
    private List<Vector3> blockPositions = new List<Vector3>();
    public Vector3[] vertices;

    private void Start(){
        for (int x = -sizeX; x < sizeX; x++){
            for (int z = -sizeZ; z < sizeZ; z++){
                Vector3 position = new Vector3(x + startPosition.x, (int) (GenNoise(x,z,20f) * noiseHeight), z + startPosition.z);
                while (position.y >= 0){
                    GameObject block = Instantiate(blockForSpawn, position, Quaternion.identity);
                    blockContainer.Add(position,block);
                    blockPositions.Add(block.transform.position);
                    block.transform.SetParent(this.transform);
                    position.y -= 1;
                }
            }
        }
        //SpawnObject();
    }

    private void Update(){
        if (Mathf.Abs(player.transform.position.x - startPosition.x) >= 1 ||
            Mathf.Abs(player.transform.position.z - startPosition.z) >= 1){
            for (int x = -sizeX; x < sizeX; x++){
                for (int z = -sizeZ; z < sizeZ; z++){
                    var position1 = player.transform.position;
                    Vector3 position = new Vector3(x + Mathf.Floor(position1.x), (int) (GenNoise(x + (int) Mathf.Floor(position1.x), z + (int) Mathf.Floor(position1.z), 20f) * noiseHeight),
                        z + Mathf.Floor(position1.z));
                    if (!blockContainer.ContainsKey(position)){
                        while (position.y >= 0){
                            GameObject block = Instantiate(blockForSpawn, position, Quaternion.identity);
                            blockContainer.Add(position, block);
                            blockPositions.Add(block.transform.position);
                            block.transform.SetParent(this.transform);
                            position.y -= 1;
                        }
                    }
                    
                }
            }
            //SpawnObject();
        }
    }

private void SpawnObject(){
    for (int i = 0; i < 20; i++){
        GameObject toPlaceObject = Instantiate(objectForSpawn, ObjectSpawnLocation(), Quaternion.identity);
    }
}

private Vector3 ObjectSpawnLocation(){
    int randIndex = Random.Range(0, blockPositions.Count);
    Vector3 newPos = new Vector3(blockPositions[randIndex].x, blockPositions[randIndex].y + 0.5f, blockPositions[randIndex].z);
    blockPositions.RemoveAt(randIndex);
    
    return newPos;
}
    
    private float GenNoise(int x, int z, float detail){
        float xNoise = (x + this.transform.position.x) / detail;
        float zNoise = (z + this.transform.position.z) / detail;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}