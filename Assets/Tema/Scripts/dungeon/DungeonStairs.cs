using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStairs : MonoBehaviour
{
    private bool canTeleport;
    private Transform player;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Press E to teleport to next floor");
            player = other.transform;
            canTeleport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Too far to teleport to next floor");
            canTeleport = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTeleport) {
            int h = FindObjectOfType<DungeonGeneratorTema>().GetDungeonLevelHeight();
            player.position = transform.position + new Vector3(0, h + 1, 0);
        }
    }
}
