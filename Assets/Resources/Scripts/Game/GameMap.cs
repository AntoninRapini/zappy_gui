using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    private static GameMap _instance;
    private static object _lock = new object();
    private Dictionary<Vector2, Inventory> _tiles;
    private Vector2 _size;
    public Vector2 Size { get; set; }
    public Dictionary<Vector2, Tile> Tiles { get; private set; }
    public Dictionary<uint, GameObject> Items = new Dictionary<uint, GameObject>();
    private GameTerrainManager _terrainManager;

    public GameMap(Vector2 size)
    {
        Size = size;
        _terrainManager = new GameTerrainManager(size);
        Items.Add(0, Resources.Load("GameObjects/Food/Prefab/Food") as GameObject);
        Items.Add(1, Resources.Load("GameObjects/Stone/Prefab/Linemate") as GameObject);
        Items.Add(2, Resources.Load("GameObjects/Stone/Prefab/Deraumere") as GameObject);
        Items.Add(3, Resources.Load("GameObjects/Stone/Prefab/Sibur") as GameObject);
        Items.Add(4, Resources.Load("GameObjects/Stone/Prefab/Mendiane") as GameObject);
        Items.Add(5, Resources.Load("GameObjects/Stone/Prefab/Phiras") as GameObject);
        Items.Add(6, Resources.Load("GameObjects/Stone/Prefab/Thystame") as GameObject);
        Tiles = new Dictionary<Vector2, Tile>();
    }

    public static Vector3 MapPositionToWorld(Vector2 position, Vector2 mapSize)
    {
        return new Vector3((position.x * 10) + mapSize.x + 5, 10, (position.y * 10) + 5 + mapSize.y);
    }

    public static Vector2 WorldPositionToMap(Vector3 position, Vector2 mapSize)
    {
        return new Vector2((position.x - mapSize.x - 5) / 10, (position.z - mapSize.y - 5) / 10);
    }

    public void SetTile(Vector2 position, uint food, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame)
    {
        if (Tiles.ContainsKey(position))
        {
            Tiles[position].Clear();
        }
        Tiles[position] = new Tile(position, food, linemate, deraumere, sibur, mendiane, phiras, thystame);
    }

    public void DropItem(Vector2 position, uint resourceId)
    {
        if (!Tiles.ContainsKey(position))
        {
            Tiles[position] = new Tile(position);
        }
        Tiles[position].AddItem(1, resourceId);
    }

    public void GetItem(Vector2 position, uint resourceId)
    {
        if (Tiles.ContainsKey(position))
        {
            Tiles[position].AddItem(-1, resourceId);
        }
    }
}

public class Inventory
{
    public Dictionary<uint, uint> Items = new Dictionary<uint, uint>();

    public Inventory() : this (0, 0, 0, 0, 0, 0, 0) { }

    public Inventory(uint food, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame)
    {
        Items[0] = food;
        Items[1] = linemate;
        Items[2] = deraumere;
        Items[3] = sibur;
        Items[4] = mendiane;
        Items[5] = phiras;
        Items[6] = thystame;
    }

    public void DropItem(Vector2 position, uint id)
    {
        if (Items.ContainsKey(id) && Items[id] > 0)
        {
            GameManager.Instance.Map.DropItem(position, id);
            AddItem(-1, id);
        }
    }

    public virtual void AddItem(int quantity, uint id)
    {
        if (Items.ContainsKey(id))
        {
            if ((int)Items[id] + quantity < 0)
                Items[id] = 0;
            else if (quantity < 0)
                Items[id] -= (uint)Math.Abs(quantity);
            else
                Items[id] += (uint)quantity;
        }
    }
}

public class Tile : Inventory
{
    private List<GameObject> _spawnObjects;
    private Vector2 _position;

    public Tile(Vector2 position) : this (position, 0, 0, 0, 0, 0, 0, 0) { }

    public Tile(Vector2 position, uint food, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame) : base(food, linemate, deraumere, sibur, mendiane, phiras, thystame)
    {
        _spawnObjects = new List<GameObject>();
        _position = position;
        SpawnItems(0, food);
        SpawnItems(1, linemate);
        SpawnItems(2, deraumere);
        SpawnItems(3, sibur);
        SpawnItems(4, mendiane);
        SpawnItems(5, phiras);
        SpawnItems(6, thystame);
    }

    public void SpawnItems(uint id, uint quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newObject = (GameObject.Instantiate(GameManager.Instance.Map.Items[id]));
            _spawnObjects.Add(newObject);
            float xTilePos = (_position.x * 10) + GameManager.Instance.Map.Size.x;
            float yTilePos = (_position.y * 10) + GameManager.Instance.Map.Size.y;
            newObject.transform.position = new Vector3(UnityEngine.Random.Range(xTilePos, xTilePos + 10), 10.4f, UnityEngine.Random.Range(yTilePos, yTilePos + 10));
        }
    }

    public override void AddItem(int quantity, uint id)
    {
        if (Items.ContainsKey(id))
        {
            if ((int)Items[id] + quantity <= 0)
            {
                Items[id] = 0;
                for (int i = _spawnObjects.Count - 1; i >= 0; i--)
                {
                    if (_spawnObjects[i].GetComponent<InventoryItem>().id == id)
                    {
                        GameObject.Destroy(_spawnObjects[i]);
                        _spawnObjects.RemoveAt(i);
                    }
                }
            }
            else if (quantity < 0)
            {
                int destroyed = 0;
                Items[id] -= (uint)Math.Abs(quantity);
                for (int i = _spawnObjects.Count - 1; i >= 0 && destroyed < Math.Abs(quantity); i--)
                {
                    if (_spawnObjects[i].GetComponent<InventoryItem>().id == id)
                    {
                        GameObject.Destroy(_spawnObjects[i]);
                        _spawnObjects.RemoveAt(i);
                        destroyed++;
                    }
                }
            }
            else
            {
                Items[id] += (uint)quantity;
                SpawnItems(id, (uint)quantity);
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _spawnObjects.Count; i++)
        {
            GameObject.Destroy(_spawnObjects[i]);
        }
        _spawnObjects.Clear();
    }
}

