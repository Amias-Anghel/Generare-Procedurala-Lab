using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private DungeonGeneratorTema dungeonGenerator;
    private DungeonNPCSpawner dungeonNPCSpawner;
    private DungeonItemSpawner dungeonItemSpawner;

    void Start()
    {
        dungeonGenerator = FindObjectOfType<DungeonGeneratorTema>();
        dungeonNPCSpawner = dungeonGenerator.GetComponent<DungeonNPCSpawner>();
        dungeonItemSpawner = dungeonGenerator.GetComponent<DungeonItemSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Vector3 dungeonPos = dungeonGenerator.GetPositionOnLevel(0);
            dungeonNPCSpawner.SpawnNPCsOnLevel(true);
            dungeonItemSpawner.SpawnItemsOnLevel(true);
            other.transform.position = dungeonPos;
        }
    }
}
