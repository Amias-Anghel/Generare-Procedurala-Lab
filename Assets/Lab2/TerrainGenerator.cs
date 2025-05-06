using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    int width = 256;
    int height = 256;
    int depth = 20;
    float scale = 20f;


    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x,y] = Mathf.PerlinNoise((float)x / width * scale,(float)y / height * scale); 
            }
        }

        return heights;
    }
}
