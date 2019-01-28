using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamItem : MonoBehaviour {

    [SerializeField]
    private Text _teamName;
    [SerializeField]
    private Text _playerCount;
    private int _players = 0;

    public void SetTeamName(string name)
    {
        _teamName.text = name;
        _playerCount.text = _players.ToString();
    }

    public void AddPlayer()
    {
        _players++;
        _playerCount.text = _players.ToString();
    }

    public void RemovePlayer()
    {
        _players--;
        _playerCount.text = _players.ToString();
    }
}
