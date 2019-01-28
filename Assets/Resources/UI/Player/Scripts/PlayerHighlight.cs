using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHighlight : MonoBehaviour
{
    [SerializeField]
    private Text _teamName;
    [SerializeField]
    private Text _food;
    [SerializeField]
    private Text _linemate;
    [SerializeField]
    private Text _deraumere;
    [SerializeField]
    private Text _sibur;
    [SerializeField]
    private Text _mendiane;
    [SerializeField]
    private Text _phiras;
    [SerializeField]
    private Text _thystame;


    public void UpdateFocus(string team, uint food, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame)
    {
        _teamName.text = "Player from team " + team;
        _food.text = food.ToString();
        _linemate.text = linemate.ToString();
        _deraumere.text = deraumere.ToString();
        _sibur.text = sibur.ToString();
        _mendiane.text = mendiane.ToString();
        _phiras.text = phiras.ToString();
        _thystame.text = thystame.ToString();
    }
}
