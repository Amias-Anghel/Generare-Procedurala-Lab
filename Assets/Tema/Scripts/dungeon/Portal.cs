using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private DungeonGeneratorTema dungeonGenerator;

    void Start()
    {
        dungeonGenerator = FindObjectOfType<DungeonGeneratorTema>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Vector3 dungeonPos = dungeonGenerator.GetPositionOnLevel(0);
            other.transform.position = dungeonPos;
        }
    }
}
