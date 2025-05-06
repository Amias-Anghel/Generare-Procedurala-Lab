using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPC", order = 1)]
public class NPC : ScriptableObject
{
    public string npc_name = "";
    public float life;
    public float damage;
    public npc_class npc_Class;

    public enum npc_class {
        rogue, fighter, druid, barbarian, bard, wizard, 
        farmer, shopkeeper, villager
    }

    public void Clear() {
        npc_name = "";
        life = 0;
        damage = 0;
    }
}
