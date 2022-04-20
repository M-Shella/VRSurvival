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
    public int wd = 4;

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

    public Transform player;

    Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
    private int lastX;
    private int lastZ;

    private float nextActionTime = 0f;
    private bool delay;

    void Start() {
        delay = true;
        Generate();
        StartCoroutine(SomeDelay());
    }

    private void Update() {
        if (!delay) SetLastPositionOnChunk();
    }

    void Generate() {
        loadingScreen.SetActive(true);

        for (int x = -WorldSizeInChunks*5; x < WorldSizeInChunks*5; x++) {
            for (int z = -WorldSizeInChunks*5; z < WorldSizeInChunks*5; z++) {
                Vector3Int chunkPos = new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth);
                chunks.Add(chunkPos, new Chunk(chunkPos));
                chunks[chunkPos].chunkObject.transform.SetParent(World.Instance.transform);
                chunks[chunkPos].chunkObject.SetActive(false);
            }
        }
        
        //Foreach chunk spawn following
        foreach (var chunk in chunks) {
            //Trava
            for (int i = 0; i < Random.Range(100, 300); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var grass1 = Instantiate(grasses[Random.Range(0, grasses.Length)],
                    new Vector3(chunk.Key.x + randomX,
                        GameData.GetTerrainHeight(chunk.Key.x + randomX, chunk.Key.z + randomZ) + 0.5f,
                        chunk.Key.z + randomZ), Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                grass1.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Stromy
            foreach (var tree in trees) {
                for (int i = 0; i < Random.Range(0, NumberOfTrees); i++) {
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var trees1 = Instantiate(tree,
                        new Vector3(chunk.Key.x + randomX,
                            GameData.GetTerrainHeight(chunk.Key.x + randomX, chunk.Key.z + randomZ),
                            chunk.Key.z + randomZ), Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                    trees1.transform.parent = chunk.Value.chunkObject.transform;
                }
            }

            //Kamney
            foreach (var rock in rocks) {
                // ReSharper disable once PossibleLossOfFraction
                for (int i = 0; i < Mathf.Floor(Random.Range(0, NumberOfRocks) / rocks.Length); i++) {
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var rocks1 = Instantiate(rock,
                        new Vector3(chunk.Key.x + randomX,
                            GameData.GetTerrainHeight(chunk.Key.x + randomX, chunk.Key.z + randomZ),
                            chunk.Key.z + randomZ),
                        Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                    rocks1.transform.parent = chunk.Value.chunkObject.transform;
                }
            }

            //Diamanty
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var diamonds = Instantiate(diamond,
                    new Vector3(chunk.Key.x + randomX, Random.Range(15, 0), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                diamonds.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Copper
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coppers = Instantiate(copper,
                    new Vector3(chunk.Key.x + randomX, Random.Range(45, 15), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coppers.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Silver
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var silvers = Instantiate(silver,
                    new Vector3(chunk.Key.x + randomX, Random.Range(64, 0), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                silvers.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Emerald
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var emeralds = Instantiate(emerald,
                    new Vector3(chunk.Key.x + randomX, Random.Range(15, 0), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                emeralds.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Coal
            for (int i = 0; i < Random.Range(20, 100); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coals = Instantiate(coal,
                    new Vector3(chunk.Key.x + randomX, Random.Range(64, 0), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coals.transform.parent = chunk.Value.chunkObject.transform;
            }

            //Gold
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var golds = Instantiate(gold,
                    new Vector3(chunk.Key.x + randomX, Random.Range(35, 5), chunk.Key.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                golds.transform.parent = chunk.Value.chunkObject.transform;
            }
        }
        
        for (int x = -WorldSizeInChunks; x < WorldSizeInChunks; x++) {
            for (int z = -WorldSizeInChunks; z < WorldSizeInChunks; z++) {
                Vector3Int chunkPos = new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth);
                chunks[chunkPos].chunkObject.SetActive(true);
            }
        }
        

        loadingScreen.SetActive(false);
    }

    public Chunk GetChunkFromVector3(Vector3 pos) {
        int x = (int) pos.x;
        int y = (int) pos.y;
        int z = (int) pos.z;

        return chunks[new Vector3Int(x, y, z)];
    }

    public Chunk GetRandomChunk() {
        return GetChunkFromVector3(new Vector3(Random.Range(0, WorldSizeInChunks) * GameData.ChunkWidth, 0,
            Random.Range(0, WorldSizeInChunks) * GameData.ChunkWidth));
    }

    private Vector3 GetRandomPositionOnChunk() {
        var x = Random.Range(0, GameData.ChunkWidth * WorldSizeInChunks);
        var z = Random.Range(0, GameData.ChunkWidth * WorldSizeInChunks);
        return new Vector3(x, GameData.GetTerrainHeight(x, z) + 0.5f, z);
    }

    private void SetLastPositionOnChunk() {
        if (Mathf.FloorToInt(player.position.x / GameData.ChunkWidth) * GameData.ChunkWidth != lastX ||
            Mathf.FloorToInt(player.position.z / GameData.ChunkWidth) * GameData.ChunkWidth != lastZ) {
            CheckAround();
        }

        lastX = Mathf.FloorToInt(player.position.x / GameData.ChunkWidth) * GameData.ChunkWidth;
        lastZ = Mathf.FloorToInt(player.position.z / GameData.ChunkWidth) * GameData.ChunkWidth;
        delay = true;
        StartCoroutine(SomeDelay());
    }

    private void CheckAround() {
        var playerX = Mathf.RoundToInt(player.position.x / GameData.ChunkWidth);
        var playerZ = Mathf.RoundToInt(player.position.z / GameData.ChunkWidth);
        var maxx = wd + playerX;
        var maxz = wd + playerZ;
        var minx = playerX - wd;
        var minz = playerZ - wd;

        for (int x = minx; x <= maxx; x++) {
            for (int z = minz; z <= maxz; z++) {
               
                if (x == minx || z == minz || x == maxx || z == maxz) {
                    if (!chunks.ContainsKey(new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth))) {
                        Vector3Int chunkPos = new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth);
                        chunks.Add(chunkPos, new Chunk(chunkPos));
                        chunks[chunkPos].chunkObject.transform.SetParent(World.Instance.transform);
                        GenerateNature(chunkPos, chunks[chunkPos].chunkObject);
                        chunks[chunkPos].chunkObject.SetActive(false);
                    }
                    chunks[new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth)].chunkObject
                        .SetActive(false);
                }
                else {
                    if (!chunks.ContainsKey(new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth))) {
                        Vector3Int chunkPos = new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth);
                        chunks.Add(chunkPos, new Chunk(chunkPos));
                        chunks[chunkPos].chunkObject.transform.SetParent(World.Instance.transform);
                        GenerateNature(chunkPos, chunks[chunkPos].chunkObject);
                    }
                    chunks[new Vector3Int(x * GameData.ChunkWidth, 0, z * GameData.ChunkWidth)].chunkObject
                        .SetActive(true);
                }
            }
        }
    }

    private IEnumerator SomeDelay() {
        yield return new WaitForSeconds(1);
        delay = false;
    }

    private void GenerateNature( Vector3Int chunkPos, GameObject chunkObject ) {
         //Trava
            for (int i = 0; i < Random.Range(100, 300); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var grass1 = Instantiate(grasses[Random.Range(0, grasses.Length)],
                    new Vector3(chunkPos.x + randomX,
                        GameData.GetTerrainHeight(chunkPos.x + randomX, chunkPos.z + randomZ) + 0.5f,
                        chunkPos.z + randomZ), Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                grass1.transform.parent = chunkObject.transform;
            }

            //Stromy
            foreach (var tree in trees) {
                for (int i = 0; i < Random.Range(0, NumberOfTrees); i++) {
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var trees1 = Instantiate(tree,
                        new Vector3(chunkPos.x + randomX,
                            GameData.GetTerrainHeight(chunkPos.x + randomX, chunkPos.z + randomZ),
                            chunkPos.z + randomZ), Quaternion.Euler(0f, Random.Range(-180, 180), 0f));
                    trees1.transform.parent = chunkObject.transform;
                }
            }

            //Kamney
            foreach (var rock in rocks) {
                // ReSharper disable once PossibleLossOfFraction
                for (int i = 0; i < Mathf.Floor(Random.Range(0, NumberOfRocks) / rocks.Length); i++) {
                    var randomX = Random.Range(0, GameData.ChunkWidth);
                    var randomZ = Random.Range(0, GameData.ChunkWidth);
                    var rocks1 = Instantiate(rock,
                        new Vector3(chunkPos.x + randomX,
                            GameData.GetTerrainHeight(chunkPos.x + randomX, chunkPos.z + randomZ)+0.3f,
                            chunkPos.z + randomZ),
                        Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                    rocks1.transform.parent = chunkObject.transform;
                }
            }

            //Diamanty
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var diamonds = Instantiate(diamond,
                    new Vector3(chunkPos.x + randomX, Random.Range(15,0), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                diamonds.transform.parent = chunkObject.transform;
            }

            //Copper
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coppers = Instantiate(copper,
                    new Vector3(chunkPos.x + randomX, Random.Range(45,15), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coppers.transform.parent = chunkObject.transform;
            }

            //Silver
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var silvers = Instantiate(silver,
                    new Vector3(chunkPos.x + randomX, Random.Range(64, 0), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                silvers.transform.parent = chunkObject.transform;
            }

            //Emerald
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var emeralds = Instantiate(emerald,
                    new Vector3(chunkPos.x + randomX, Random.Range(15,0), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                emeralds.transform.parent = chunkObject.transform;
            }

            //Coal
            for (int i = 0; i < Random.Range(20, 100); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var coals = Instantiate(coal,
                    new Vector3(chunkPos.x + randomX, Random.Range(64, 0), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                coals.transform.parent = chunkObject.transform;
            }

            //Gold
            for (int i = 0; i < Random.Range(5, NumberOfDiamoonds); i++) {
                var randomX = Random.Range(0, GameData.ChunkWidth);
                var randomZ = Random.Range(0, GameData.ChunkWidth);
                var golds = Instantiate(gold,
                    new Vector3(chunkPos.x + randomX, Random.Range(35, 5), chunkPos.z + randomZ),
                    Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
                golds.transform.parent = chunkObject.transform;
            }
    }
}