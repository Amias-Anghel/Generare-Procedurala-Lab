using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string item_name = "";
    public string type = "";
    public float durability;
    public float damage;
    public rarity item_rarity;
    public ability special_ability = ability.none;

    public enum rarity {
        common, unusual, rare, legendary, mythical
    
    };
    public enum ability {
        none,
        poison, lifesteal, fire_damage, ice_slow
    };


    public void Clear() {
        item_name = "";
        type = "";
        durability = 0;
        damage = 0;
        item_rarity = rarity.common;
        special_ability = ability.none;
    }
}
