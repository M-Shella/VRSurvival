using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk{
    public GameObject chunkObject;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;

    Vector3Int chunkPosition;

    TerrainPoint[,,] terrainMap;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    int width { get { return GameData.ChunkWidth; } }
    int height { get { return GameData.ChunkHeight; } }
    float terrainSurface { get { return GameData.terrainSurface; } }

    public Chunk (Vector3Int _position) {

        chunkObject = new GameObject();
        chunkObject.name = string.Format("Chunk {0}, {1}", _position.x, _position.z);
        chunkPosition = _position;
        chunkObject.transform.position = chunkPosition;

        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshCollider = chunkObject.AddComponent<MeshCollider>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("Materials/Terrain");
        meshRenderer.material.SetTexture("_TexArr", World.Instance.terrainTexArray);
        chunkObject.transform.tag = "Terrain";
        chunkObject.layer = 10;
        terrainMap = new TerrainPoint[width + 1, height + 1, width + 1];
        PopulateTerrainMap();
        CreateMeshData();

    }

    void CreateMeshData() {

        ClearMeshData();

        // Loop zkrz každou kostku v terénu
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < width; z++) {

                    // MarchCube funkce pro kžadou kostku.
                    MarchCube(new Vector3Int(x, y, z));

                }
            }
        }

        BuildMesh();

    }

    void PopulateTerrainMap () {
        for (int x = 0; x < width + 1; x++) {
            for (int z = 0; z < width + 1; z++) {
                for (int y = 0; y < height + 1; y++) {
                    float thisHeight = GameData.GetTerrainHeight(x + chunkPosition.x, z + chunkPosition.z);

                    terrainMap[x, y, z] = new TerrainPoint ((float)y - thisHeight, Random.Range(0, World.Instance.terrainTextures.Length));
                }
            }
        }
    }

    void MarchCube (Vector3Int position) {

        // Zjistí pozici vrcholu “kostky“ na pozici získané z parametru
        float[] cube = new float[8];
        for (int i = 0; i < 8; i++) cube[i] = SampleTerrain(position + GameData.CornerTable[i]);

        // Zjistí index konfigurace (jestli je vrchol v terénu nebo mimo něj)
        int configIndex = GetCubeConfiguration(cube);

        // Pokud je index 0 nebo 255 tak je vrchol buď někde uvnitř terénu nebo mimo něj
        if (configIndex == 0 || configIndex == 255) return;

        // Proloopuje skrz všechny trojúhelníky v kostce (každá kostka má max 5 trojúhelníků a každý trojúhelník 3 vrcholy)
        int edgeIndex = 0;
        for(int i = 0; i < 5; i++) {
            for(int p = 0; p < 3; p++) {

                // Získá aktuální index vrcholu trojůhelníku
                int indice = GameData.TriangleTable[configIndex, edgeIndex];

                // Pokud je index -1, tak už není více vrcholů 
                if (indice == -1) return;

                // získá 2 vektory který udávají vrchol kudy se “kostka prosekne“ z tabulky vrcholů
                Vector3 vert1 = position + GameData.CornerTable[GameData.EdgeIndexes[indice, 0]];
                Vector3 vert2 = position + GameData.CornerTable[GameData.EdgeIndexes[indice, 1]];

                Vector3 vertPosition;

                // Získá vrchol začítátku a konce hrany co bude proseknuta
                float vert1Sample = cube[GameData.EdgeIndexes[indice, 0]];
                float vert2Sample = cube[GameData.EdgeIndexes[indice, 1]];

                // Vypočítá bod kudy terén projde
                float difference = vert2Sample - vert1Sample;

                // pokud je 0 terén jde středem hrany
                if (difference == 0) difference = terrainSurface;
                else difference = (terrainSurface - vert1Sample) / difference;

                // A získá vektor kudy přesně vrchol prochází
                vertPosition = vert1 + ((vert2 - vert1) * difference);

                // Vrchol přidá do seznamu vrcholů trojůhelníku meshe a inkrementuje edgeIndex
                triangles.Add(VertForIndice(vertPosition, position));

                edgeIndex++;

            }
        }
    }

    int GetCubeConfiguration (float[] cube) {

        int configurationIndex = 0;
        for (int i = 0; i < 8; i++) {
            
            if (cube[i] > terrainSurface)
                configurationIndex |= 1 << i;
            
        }

        return configurationIndex;

    }

    public void PlaceTerrain (Vector3 pos) {

        Vector3Int v3Int = new Vector3Int(Mathf.CeilToInt(pos.x), Mathf.CeilToInt(pos.y), Mathf.CeilToInt(pos.z));
        v3Int -= chunkPosition;
        terrainMap[v3Int.x, v3Int.y, v3Int.z].dstToSurface = 0f;
        CreateMeshData();

    }

    public void RemoveTerrain (Vector3 pos) {
        Vector3Int v3Int = new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
        v3Int -= chunkPosition;
        if (terrainMap[v3Int.x, v3Int.y, v3Int.z].dstToSurface == 1 || terrainMap[v3Int.x, v3Int.y, v3Int.z] == null){
            return;
        }
        
        terrainMap[v3Int.x, v3Int.y, v3Int.z].dstToSurface = 1f;
        CreateMeshData();
    }

    float SampleTerrain (Vector3Int point) {

        return terrainMap[point.x, point.y, point.z].dstToSurface;

    }

    int VertForIndice (Vector3 vert, Vector3Int point) {
        // Loop zkrze všechny vrcholy v aktuálním seznamu vrcholů
        for (int i = 0; i < vertices.Count; i++) {

            // Pokud najdeme vrchol kterej je stejný z vrcholem z parametru tak vrátíme jeho index
            if (vertices[i] == vert)
                return i;

        }
        
        // Pokud žádnej nenajdeme přidáme ho do seznamu a vrátíme poslední index
        vertices.Add(vert);
        uvs.Add(new Vector2(terrainMap[point.x, point.y, point.z].textureId, 0));
        return vertices.Count - 1;

    }

    void ClearMeshData () {

        vertices.Clear();
        triangles.Clear();
        uvs.Clear();

    }

    void BuildMesh () {

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

    }
}