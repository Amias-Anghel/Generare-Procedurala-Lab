using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public List<Item> items;

    private string GetRandomName() {
        string[] pattern = {
            "@*@**@", 
            "@#@*@@*", 
            "*@@#@", 
            "*@*#@*", 
            "*@*@@#", 
            "@*@*@@*", 
            "#@*@*@", 
            "@*@*@", 
            "@*@#@@*", 
            "*@#@@*", 
        };

        if (Random.value > 0.2f) {
            return GeneratorUtils.GenNameWithTitle(pattern[Random.Range(0, pattern.Length)]);
        }

        return GeneratorUtils.GenNameFromPattern(pattern[Random.Range(0, pattern.Length)]);
    }

    private string GetRandomType() {
        string[] types = {
            "sword", "armor plate", "shield", "flank vest", "bow", "dagger", 
            "chainmail", "war hammer", "iron helm", "leather boots", "gauntlets", 
            "crossbow", "quiver", "throwing knife", "battle axe", "spear", "pike", 
            "halberd", "mace", "buckler", "greaves", "vambrace", "scimitar", 
            "rapier", "sling", "javelin", "tower shield", "scale armor", 
            "hooded cloak", "reinforced belt", "spiked collar", "arm guard", 
            "leg plates", "chest guard", "skull cap", "flail", "morningstar", 
            "longbow", "shortsword", "kite shield", "chain coif", "plate leggings", 
            "saber", "cutlass", "katana", "throwing star", "bola", "war scythe", 
            "trident", "hand cannon", "poison vial"  
        };

        return types[Random.Range(0, types.Length)];
    }

    public void GenerateItem(int index) {
        items[index].item_name = GetRandomName();
        items[index].type = GetRandomType();

        items[index].durability = Random.Range(10f, 100f);
        items[index].damage = Random.Range(5f, 100f);

        items[index].item_rarity = GetRarity();
        items[index].special_ability = Item.ability.none;

        if (items[index].item_rarity == Item.rarity.legendary || items[index].item_rarity == Item.rarity.mythical) {
            items[index].special_ability = (Item.ability)Random.Range(1, 5);
        }
    }

    private Item.rarity GetRarity() {
        float rarity = Random.value;

        if (rarity >= 0.95) {
            return Item.rarity.mythical;
        }
        else if (rarity >= 0.9) {
            return Item.rarity.legendary;
        }
        if (rarity >= 0.7) {
            return Item.rarity.rare;
        }
        if (rarity >= 0.5) {
            return Item.rarity.unusual;
        }
        return Item.rarity.common;
        
    }

    public void ClearItem(int index) {
        items[index].Clear();
    }

    public Item GetItemData(int index) {
        return items[index];
    }
}
