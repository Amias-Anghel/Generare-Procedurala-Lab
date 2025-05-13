using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    public void PlacePlayerInDungeon()
    {
        DungeonGeneratorTema dungeonGenerator = GetComponent<DungeonGeneratorTema>();
        DungeonNPCSpawner dungeonNPCSpawner = GetComponent<DungeonNPCSpawner>();
        DungeonItemSpawner dungeonItemSpawner = GetComponent<DungeonItemSpawner>();

        
        dungeonNPCSpawner.SpawnNPCsOnLevel(true);
        dungeonItemSpawner.SpawnItemsOnLevel(true);

        Vector3 dungeonPos = dungeonGenerator.GetPositionOnLevel(0);
        player.position = dungeonPos;
    }
}
