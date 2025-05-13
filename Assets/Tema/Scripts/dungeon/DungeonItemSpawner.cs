using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private DungeonGeneratorTema dungeon;
    private int playerlevel = 0;

    private List<GameObject> objects = null;


    public void SpawnItemsOnLevel(bool reset = false) {
        if (reset) playerlevel = 0;
        
        DestroyPrevObject();
        
        foreach(Item item in items) {
            Vector3 spawnPos = dungeon.GetPositionOnLevel(playerlevel) - new Vector3(0, 1.2f, 0);
            GameObject dungeonItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            objects.Add(dungeonItem);

            ItemGenerator generator = dungeonItem.transform.GetChild(1).GetChild(0).GetComponent<ItemGenerator>();
            generator.items = new List<Item>
            {
                item
            };
            generator.GenerateItem(0);
            generator.GetComponent<ItemViewerUI>().ShowItem();
            dungeonItem.transform.SetParent(transform);
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
