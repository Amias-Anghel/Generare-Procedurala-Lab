using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewerUI : MonoBehaviour
{
    private ItemGenerator itemGenerator;
    private RarityStarsEffect rarityStarsEffect;

    [SerializeField] private TMP_Text display_name;
    [SerializeField] private TMP_Text display_type;
    [SerializeField] private TMP_Text display_damage;
    [SerializeField] private TMP_Text display_durability;
    [SerializeField] private TMP_Text display_ability;
    [SerializeField] private List<Slider> raritySliders;
    [SerializeField] private List<item_ability> item_Abilities;
    [SerializeField] private List<rarity_color> rarities_colors;

    [Serializable] struct item_ability {
        public Item.ability ability;
        public string text;
    }

    [Serializable] struct rarity_color {
        public Item.rarity rarity;
        public Color color;
    }

    int index = 0;

    void Start()
    {
        itemGenerator = GetComponent<ItemGenerator>();
        rarityStarsEffect = GetComponent<RarityStarsEffect>();
        ShowItem();
    }

    public void NextItem(){
        if (++index >= itemGenerator.items.Count) {
            index = 0;
        }
        ShowItem();
    }

    public void PreItem(){
        if (--index < 0) {
            index = itemGenerator.items.Count - 1;
        }
        ShowItem();
    }

    public void GeneratItem() {
        itemGenerator.GenerateItem(index);
        ShowItem();
    }

    public void ClearItem() {
        itemGenerator.ClearItem(index);
        ShowItem();
    }

    public void ShowItem() {
        Item data = itemGenerator.GetItemData(index);

        if (data.item_name == "") {
            display_ability.text = "";
            display_name.text = "";
            display_durability.text = "";
            display_damage.text = "";
            display_type.text = "";
            SetRarityStars(Item.rarity.common);
            ChangeToColor(rarities_colors[0].color);
            return;
        }

        display_ability.text = "";
        display_name.text = data.item_name;
        display_durability.text = data.durability.ToString();
        display_type.text = data.type;
        display_damage.text = data.damage.ToString();
        SetRarityStars(data.item_rarity);

        foreach(item_ability ability in item_Abilities) {
            if (ability.ability == data.special_ability) {
                display_ability.text = ability.text;
                break;
            }
        }

        foreach(rarity_color color in rarities_colors) {
            if (color.rarity == data.item_rarity) {
                ChangeToColor(color.color);
            }
        }
    }

    private void ChangeToColor(Color color) {
        display_ability.color = color;
        display_name.color = color;
    }

    private void SetRarityStars(Item.rarity rarity) {
        rarityStarsEffect.SetAnimation(false);
        float proccent = 0.2f;
        switch(rarity) {
            case Item.rarity.common:
                proccent = 0.2f;
                break;
            case Item.rarity.unusual:
                proccent = 0.4f;
                break;
            case Item.rarity.rare:
                proccent = 0.6f;
                break;
            case Item.rarity.legendary:
                proccent = 0.8f;
                break;
            case Item.rarity.mythical:
                rarityStarsEffect.SetAnimation(true);
                proccent = 1f;
                break;
        }

        foreach(Slider s in raritySliders) {
            s.value = proccent;
        }
    }
}
