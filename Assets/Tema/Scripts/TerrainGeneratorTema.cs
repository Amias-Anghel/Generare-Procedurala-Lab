using UnityEngine;

public class TerrainGeneratorTema : MonoBehaviour
{
    int width = 1024;
    int height = 1024;
    int depth = 20;
    float scale = 20f;

    private int seed;
    private Vector2 offset;

    public static System.Action<Terrain> OnTerrainGenerated; 

    void Start()
    {
        seed = Random.Range(0, 100000);
        offset = new Vector2(seed, seed);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        OnTerrainGenerated?.Invoke(terrain);
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
                heights[x,y] = SampleNoise(x, y); 
            }
        }

        return heights;
    }

    float SampleNoise(int x, int y) {
        return Mathf.PerlinNoise(
            offset.x + (float)x / width * scale,
            offset.y + (float)y / height * scale
        );
    }

}
