using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    public List<NPC> npc_slots;

    private string GetRandomName() {
        string[] pattern = {
            "@*@*", 
            "@#@*", 
            "*@#@", 
            "*@#@*", 
            "*@*@", 
            "@*@*", 
            "@*@*@", 
            "@*@*@", 
            "@*@#@", 
            "*@#", 
        };

        if (Random.value > 0.9f) {
            return GeneratorUtils.GenNameWithTitle(pattern[Random.Range(0, pattern.Length)]);
        }

        return GeneratorUtils.GenNameFromPattern(pattern[Random.Range(0, pattern.Length)]);
    }

    public void GenerateNPC(int index) {
        npc_slots[index].npc_name = GetRandomName();
        npc_slots[index].life = Random.Range(10f, 100f);
        npc_slots[index].damage = Random.Range(5f, 100f);

        npc_slots[index].npc_Class = (NPC.npc_class)Random.Range(0, 9);
    }

    public void ClearNPC(int index) {
        npc_slots[index].Clear();
    }


    public void RenameNPC(int index, string newName) {
        npc_slots[index].npc_name = newName;
    }
    
    public NPC GetNPCData(int index) {
        return npc_slots[index];
    }
}
