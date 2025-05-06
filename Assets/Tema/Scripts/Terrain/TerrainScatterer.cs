using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScatterer : MonoBehaviour
{
    public GameObject[] objToSpawn;
    public int spawnCount = 500;
    public bool randomYRotation = true;

    public Vector2 scaleRange = new Vector2(1f, 1f);

    void Awake()
    {
        TerrainGeneratorTema.OnTerrainGenerated += Scatter;
    }

    public void Scatter(Terrain terrain) {
        var terrainData = terrain.terrainData;
        var terrainPos = terrain.transform.position;

        for (int i = 0; i < spawnCount; i++)
        {
            float randomX = Random.Range(0f, terrainData.size.x);
            float randomZ = Random.Range(0f, terrainData.size.z);

            Vector3 worldPos = new Vector3(
                terrainPos.x + randomX,
                0, 
                terrainPos.z + randomZ
            );

            float y = terrain.SampleHeight(worldPos) + terrainPos.y;
            worldPos.y = y;

            var spawned = Instantiate(objToSpawn[Random.Range(0, objToSpawn.Length)], worldPos, Quaternion.identity, transform);

            if (randomYRotation)
            {
                spawned.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
            }

            float scale = Random.Range(scaleRange.x, scaleRange.y);
            spawned.transform.localScale = Vector3.one * scale;
        }
    }
}
