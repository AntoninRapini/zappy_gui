using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [HideInInspector]
    public int FocusedId;
    [SerializeField]
    private GameObject _teamsContainer;
    private Dictionary<string, TeamItem> _teams = new Dictionary<string, TeamItem>();
    [SerializeField]
    private PlayerHighlight _playerHighlight;
    [SerializeField]
    private Text _victoryText;
    [SerializeField]
    private Slider _timeUnitSlider;
    [SerializeField]
    private Text _timeUnitText;
    [HideInInspector]
    public float TimeUnit
    {
        private get { return 0; }
        set
        {
            _timeUnitText.text = value.ToString();
            _timeUnitSlider.value = value;
        }
    }

    private void Start()
    {
        _victoryText.text = "";
    }


    public void AddTeam(string teamName)
    {
        GameObject newTeamObject = GameObject.Instantiate(Resources.Load("UI/Team/TeamItem") as GameObject);
        TeamItem newTeamItem = newTeamObject.GetComponent<TeamItem>();
        _teams[teamName] = newTeamItem;
        newTeamItem.SetTeamName(teamName);
        newTeamObject.transform.SetParent(_teamsContainer.transform);
    }

    public void AddPlayer(string teamName)
    {
        if (_teams.ContainsKey(teamName))
        {
            _teams[teamName].AddPlayer();
        }
    }

    public void RemovePlayer(string teamName)
    {
        _teams[teamName].RemovePlayer();
    }

    public void ShowPlayer(uint id, string team, uint food, uint linemate, uint deraumere, uint sibur, uint mendiane, uint phiras, uint thystame)
    {
        _playerHighlight.gameObject.SetActive(true);
        FocusedId = (int)id;
        _playerHighlight.UpdateFocus(team, food, linemate, deraumere, sibur, mendiane, phiras, thystame);
    }

    public void ShowVictoryScreen(string teamName)
    {
        _victoryText.text = "Team " + teamName + " wins !";
    }
}
