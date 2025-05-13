using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNPCSpawner : MonoBehaviour
{
    [SerializeField] private List<NPC> npcs;
    [SerializeField] private GameObject npcPrefab;

    [SerializeField] private DungeonGeneratorTema dungeon;
    private int playerlevel = 0;
    private List<GameObject> objects = null;

    public void SpawnNPCsOnLevel(bool reset = false) {
        if (reset) playerlevel = 0;
        DestroyPrevObject();

        foreach(NPC npc in npcs) {
            Vector3 spawnPos = dungeon.GetPositionOnLevel(playerlevel) - new Vector3(0, 0.7f, 0);
            GameObject dungeonNPC = Instantiate(npcPrefab, spawnPos, Quaternion.identity);
            objects.Add(dungeonNPC);

            dungeonNPC.GetComponent<NPCLookAt>().isEnemy = true;
            NPCGenerator generator = dungeonNPC.transform.GetChild(1).GetChild(0).GetComponent<NPCGenerator>();
            generator.npc_slots = new List<NPC>
            {
                npc
            };
            generator.GenerateNPC(0);
            generator.GetComponent<NPCViewerUI>().ShowNPC();
            dungeonNPC.transform.SetParent(transform);
        }

        playerlevel++;
    }

    private void DestroyPrevObject() {
        if (objects != null) {
            foreach (GameObject o in objects) {
                if (o != null)
                    Destroy(o);
            }
        }

        objects = new List<GameObject>();
    }
}
