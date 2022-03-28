using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPoint{
    public float dstToSurface = 1f;

    public int textureId = 0;

    public TerrainPoint(float dst, int tex){
        dstToSurface = dst;
        textureId = tex;
    }
}
