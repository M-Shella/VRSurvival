using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour {
    public int WorldSizeInChunks = 10;

    public int NumberOfTrees = 20;
    public int NumberOfRocks = 20;
    public int NumberOfDiamoonds = 20;

    public GameObject[] rocks;
    public GameObject[] trees;
    public GameObject[] grasses;
    public GameObject diamond;
    public GameObject silver;
    public GameObject copper;
    public GameObject emerald;
    public GameObject gold;
    public GameObject coal;
    public GameObject loadingScreen;
    
    Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();

    void Start() {
        Generate();
    }

    void Generate () {

        loadingScreen.SetActive(true);

        for (int x = 0; x < WorldSizeInChunks; x++) {
            for (int z = 0; z < WorldSizeInChunks; z++) {

                Vector3Int chunkPos = new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth);
                chunks.Add(chunkPos, new Chunk(chunkPos));
                chunks[chunkPos].chunkObject.transform.SetParent(World.Instance.transform);
            }
        }

        //Foreach chunk spawn following
        foreach (var chunk in chunks) {
            //Trava
            for (int i = 0; i < Random.Range(100, 300); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var grass1 = Instantiate(grasses[Random.Range(0, grasses.Length)], new Vector3(chunk.Key.x + randomX,
                        GameData.GetTerrainHeight(chunk.Key.x +randomX,chunk.Key.z +randomZ)+0.5f,
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                grass1.transform.parent = gameObject.transform;
            }
            
            //Stromy
            foreach (var tree in trees) {
                for (int i = 0; i < Random.Range(0, NumberOfTrees); i++) {
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var trees1 = Instantiate(tree, new Vector3(chunk.Key.x + randomX,
                            GameData.GetTerrainHeight(chunk.Key.x +randomX,chunk.Key.z +randomZ),
                            chunk.Key.z + randomZ), 
                        Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                    trees1.transform.parent = gameObject.transform;
                }
            }
           
            //Kamney
            foreach (var rock in rocks) {
                // ReSharper disable once PossibleLossOfFraction
                for (int i = 0; i < Mathf.Floor( Random.Range(0, NumberOfRocks)/rocks.Length); i++){
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var rocks1 = Instantiate(rock, new Vector3(chunk.Key.x + randomX,
                                                                               GameData.GetTerrainHeight(chunk.Key.x +randomX,chunk.Key.z +randomZ),
                                                                               chunk.Key.z + randomZ), 
                                                             Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                    rocks1.transform.parent = gameObject.transform;
                }
            }
            //Diamanty
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var diamonds = Instantiate(diamond, new Vector3(chunk.Key.x + randomX,
                                                                             Random.Range(55, 40),
                                                                             chunk.Key.z + randomZ), 
                                                            Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                diamonds.transform.parent = gameObject.transform;
            }
            //Copper
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coppers = Instantiate(copper, new Vector3(chunk.Key.x + randomX,
                        Random.Range(55, 40),
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coppers.transform.parent = gameObject.transform;
            }
            //Silver
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var silvers = Instantiate(silver, new Vector3(chunk.Key.x + randomX,
                        Random.Range(64, 0),
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                silvers.transform.parent = gameObject.transform;
            }
            //Emerald
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var emeralds = Instantiate(emerald, new Vector3(chunk.Key.x + randomX,
                        Random.Range(55, 40),
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                emeralds.transform.parent = gameObject.transform;
            }
            //Coal
            for (int i = 0; i < Random.Range(20, 100); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coals = Instantiate(coal, new Vector3(chunk.Key.x + randomX,
                        Random.Range(64, 0),
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coals.transform.parent = gameObject.transform;
            }
            //Gold
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var golds = Instantiate(gold, new Vector3(chunk.Key.x + randomX,
                        Random.Range(55, 40),
                        chunk.Key.z + randomZ), 
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                golds.transform.parent = gameObject.transform;
            }
        }
        
        loadingScreen.SetActive(false);
    }

    public Chunk GetChunkFromVector3 (Vector3 pos) {

        int x = (int)pos.x;
        int y = (int)pos.y;
        int z = (int)pos.z;
        
        return chunks[new Vector3Int(x, y, z)];

    }

    public Chunk GetRandomChunk(){
        return GetChunkFromVector3(new Vector3(Random.Range(0,WorldSizeInChunks)*GameData.ChunkWidth, 0, Random.Range(0, WorldSizeInChunks)*GameData.ChunkWidth));
    }

    private Vector3 GetRandomPositionOnChunk(){
        var x = Random.Range(0, GameData.ChunkWidth*WorldSizeInChunks);
        var z = Random.Range(0, GameData.ChunkWidth*WorldSizeInChunks);
        return new Vector3(x, GameData.GetTerrainHeight(x, z) + 0.5f, z);
    }
    
}