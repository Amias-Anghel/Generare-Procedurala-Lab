using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGeneratorTema : MonoBehaviour
{
    [SerializeField] private GameObject wall, tile, stairs;

    [SerializeField] private int dungeonWidth = 64, dungeonHeight = 64;
    [SerializeField] private int roomSizeMin = 6, roomSizeMax = 12;
    [SerializeField] private int maxIterations = 5;

    [SerializeField] private int dungeonLevels = 3, dungeonLevelHeight = 2;
    private int currentDungeonLevel = 0;

    private List<Room> rooms;
    private List<List<Room>> allRooms;
    private List<Vector2> tilesPositions;


    public struct Room {
        public int x, y;
        public int width, height;

        public Room(int width, int height, int x, int y) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }

    void Start()
    {
        Vector2 stairs = Vector2.negativeInfinity;

        allRooms = new List<List<Room>>();

        for (currentDungeonLevel = 0; currentDungeonLevel < dungeonLevels; currentDungeonLevel++) {
            Transform levelParent = new GameObject("level"+currentDungeonLevel).transform;
            stairs = GenerateDungeonLevel(stairs, levelParent);
            levelParent.SetParent(transform);
        }

        transform.position = new Vector3(0, 1000, 0);
    }

    private Vector2 GenerateDungeonLevel(Vector2 prevStairs, Transform levelParent) {
        Vector2 nextStairs = Vector2.positiveInfinity;
        
        rooms = new List<Room>();
        tilesPositions = new List<Vector2>();
        
        // rect area -> pt verificarea divizarii spatiului
        BSP_GenerateRooms (maxIterations, new Rect(0, 0, dungeonWidth, dungeonHeight));

        allRooms.Add(rooms);

        if (prevStairs.x != Vector2.negativeInfinity.x) {
            
            rooms.Add(new Room(roomSizeMin, roomSizeMin, (int)prevStairs.x, (int)prevStairs.y));
        }

        // set next stairs pos
        if (currentDungeonLevel < dungeonLevels - 1) {
            int newStairsX = Random.Range(0, dungeonWidth);
            int newStairsY = Random.Range(0, dungeonHeight);
            
            nextStairs = new Vector2(newStairsX, newStairsY);
            rooms.Add(new Room(roomSizeMin, roomSizeMin, newStairsX, newStairsY));
        }


        foreach(Room room in rooms) {
            CreateRoom(room, levelParent, prevStairs, nextStairs);
        }

        GenerateCorridors(levelParent, prevStairs, nextStairs);

        
        AddWalls(levelParent);

        return nextStairs;
    }

    private void BSP_GenerateRooms(int iteration, Rect area) {
        if (iteration == 0 || (area.width <= roomSizeMax && area.height <= roomSizeMax)) {
            int roomWidth = Random.Range(roomSizeMin, (int)area.width);
            int roomHeight = Random.Range(roomSizeMin, (int)area.height);
            int roomX = (int)area.x + Random.Range(0, (int)area.width - roomWidth);
            int roomY = (int)area.y + Random.Range(0, (int)area.height - roomHeight);

            rooms.Add(new Room(roomWidth, roomHeight, roomX,  roomY));
        } else {
            bool splitHorizontal = area.width / area.height >= 1.25f;
            splitHorizontal = splitHorizontal ? splitHorizontal : area.height / area.width >= 1.25f;
            splitHorizontal = splitHorizontal ? splitHorizontal : Random.value > 0.5f;
            
            if (splitHorizontal) {
                int split = Random.Range((int)area.x + roomSizeMin, (int)area.xMax - roomSizeMin);
                BSP_GenerateRooms(iteration - 1, new Rect(area.x, area.y, split - area.x, area.height));
                BSP_GenerateRooms(iteration - 1, new Rect(split, area.y, (int)area.xMax - split, area.height));

                // corridors.Add(new Room(2, (int)area.height, split - 1,  (int)area.y));
            } else {
                int split = Random.Range((int)area.y + roomSizeMin, (int)area.yMax - roomSizeMin);
                BSP_GenerateRooms(iteration - 1, new Rect(area.x, area.y, area.width, split - area.y));
                BSP_GenerateRooms(iteration - 1, new Rect(area.x, split, area.width, (int)area.yMax - split));

                // corridors.Add(new Room((int)area.width, 2, (int)area.x, split - 1));
            }
        }
    }

    private void CreateRoom(Room room, Transform levelParent, Vector2 prevStairs, Vector2 nextStairs) {
        int h = currentDungeonLevel * dungeonLevelHeight;
        for(int x = room.x; x < room.x + room.width; x++) {
            for(int y = room.y; y < room.y + room.height; y++) {
                InstantiateTile(x, y, levelParent, prevStairs, nextStairs);
            }
        }
    }

    private void Shuffle(List<Room> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            
            Room temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void GenerateCorridors(Transform levelParent, Vector2 prevStairs, Vector2 nextStairs) {
        Shuffle(rooms);
        for (int i = 0; i < rooms.Count - 1; i++) {
            Room start = rooms[i];
            Room end = rooms[i + 1];

            int x = start.x;
            int y = start.y;
            int targetX = end.x;
            int targetY = end.y;

            while (x != targetX || y != targetY) {
                if (x != targetX) {
                    x += (x < targetX) ? 1 : -1; 
                }
                else if (y != targetY) {
                    y += (y < targetY) ? 1 : -1;
                }

                InstantiateTile(x, y, levelParent, prevStairs, nextStairs);
            }
        }
    }

    private void InstantiateTile(int x, int y, Transform levelParent, Vector2 prevStairs, Vector2 nextStairs) {
        int h = currentDungeonLevel * dungeonLevelHeight;

        if (tilesPositions.Contains(new Vector2(x, y))) {
            return;
        }

        if (prevStairs.x == x && prevStairs.y == y) {
            
        }
        else if (nextStairs.x == x && nextStairs.y == y) {
            Instantiate(stairs, new Vector3(x, h, y), Quaternion.identity).transform.SetParent(levelParent);
        } 
        else {
            Instantiate(tile, new Vector3(x, h, y), Quaternion.identity).transform.SetParent(levelParent);
        }

        tilesPositions.Add(new Vector2(x, y));
    
    }

    private void AddWalls(Transform levelParent) {
        Vector2[] directions = new Vector2[] {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1)
        };

        int h = currentDungeonLevel * dungeonLevelHeight;

        foreach(Vector2 tile in tilesPositions) {
            foreach(Vector2 dir in directions) {
                Vector2 nei = tile + dir;

                if (!tilesPositions.Contains(nei)) {
                    Instantiate(wall, new Vector3(nei.x, h, nei.y), Quaternion.identity).transform.SetParent(levelParent);
                }
            }
        }
    }

    public Vector3 GetPositionOnLevel(int level) {
        List<Room> levelRooms = allRooms[level];
        
        Room room = levelRooms[Random.Range(0, levelRooms.Count)];
        int x = Random.Range(room.x, room.width);
        int y = Random.Range(room.y, room.height);

        int h = dungeonLevelHeight * level;

        return transform.position + new Vector3(x, h + 1, y);
    }

    public int GetDungeonLevelHeight() {
        return dungeonLevelHeight;
    }
}
