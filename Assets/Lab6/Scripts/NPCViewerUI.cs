using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCViewerUI : MonoBehaviour
{ 
    [SerializeField] private NPCGenerator npcGenerator;
    [SerializeField] private TMP_InputField display_name;
    [SerializeField] private TMP_Text display_damage;
    [SerializeField] private TMP_Text display_life;
    [SerializeField] private Image display_class_img;
    [SerializeField] private TMP_Text display_class;
    [SerializeField] private Sprite empty_sprite;
    [SerializeField] private List<class_display> class_sprites;

    [Serializable] struct class_display {
        public NPC.npc_class npc_Class;
        public Sprite sprite;
        public string text;
    }

    int index = 0;

    void Start()
    {
        npcGenerator = GetComponent<NPCGenerator>();
        ShowNPC();
    }

    public void NextNPC(){
        if (++index >= npcGenerator.npc_slots.Count) {
            index = 0;
        }
        ShowNPC();
    }

    public void PrevNPC(){
        if (--index < 0) {
            index = npcGenerator.npc_slots.Count - 1;
        }
        ShowNPC();
    }

    [ContextMenu("Generate NPC")]
    public void GenerateNPC() {
        npcGenerator.GenerateNPC(index);
        ShowNPC();
    }

    public void RenameNPC( string newName) {
        npcGenerator.RenameNPC(index, newName);
        ShowNPC();
    }

    public void ClearNPC() {
        npcGenerator.ClearNPC(index);
        ShowNPC();
    }

    public void ShowNPC() {
        NPC data = npcGenerator.GetNPCData(index);

        if (data.npc_name == "") {
            display_class_img.sprite = empty_sprite;
            display_class.text = "";
            display_name.text = "";
            display_life.text = "";
            display_damage.text = "";
            return;
        }

        display_name.text = data.npc_name;
        display_life.text = data.life.ToString();
        display_damage.text = data.damage.ToString();

        foreach(class_display class_Sprite in class_sprites) {
            if (class_Sprite.npc_Class == data.npc_Class) {
                display_class_img.sprite = class_Sprite.sprite;
                display_class.text = class_Sprite.text;
                break;
            }
        }
    }
}
