using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNPCSpawner : MonoBehaviour
{
    [SerializeField] private List<NPC> npcs;
    [SerializeField] private GameObject npcPrefab;

    [SerializeField] private DungeonGeneratorTema dungeon;
    private int playerlevel = 0;


    public void SpawnNPCsOnLevel(bool reset = false) {
        if (reset) playerlevel = 0;
        
        foreach(NPC npc in npcs) {
            Vector3 spawnPos = dungeon.GetPositionOnLevel(playerlevel) - new Vector3(0, 0.7f, 0);
            GameObject dungeonNPC = Instantiate(npcPrefab, spawnPos, Quaternion.identity);
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
}
