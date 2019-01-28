using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Dictionary<uint, Player> Players = new Dictionary<uint, Player>();
    private Object _playerPrefab;

    public Team(int id, string name)
    {
        Id = id;
        Name = name;
        _playerPrefab = Resources.Load("GameObjects/Player/Prefab/Player");
        GameManager.Instance.GameUIManager.AddTeam(name);
    }

    public void AddPlayer(uint id, Player player)
    {
        Players.Add(id, player);
    }
}
