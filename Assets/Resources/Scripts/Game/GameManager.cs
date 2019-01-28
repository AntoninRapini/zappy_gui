using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static object _lock = new object();

    public static GameManager Instance
    {
        get
        {
            lock(_lock)
            {
                if (_instance == null)
                {
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<GameManager>();
                        singleton.name = "GameManagerObject";
                    }
                }
                return _instance;
            }
        }
    }

    private float _timeUnitReciprocal = 10;
    public float TimeUnitReciprocal
    {
        get { return _timeUnitReciprocal; }
        set
        {
                _timeUnitReciprocal = value;
                GameUIManager.TimeUnit = value;
                //ServerCommandHandlers.SendCmd("sst " + ((uint)value).ToString());
        }
    }

    public void SetTimeUnitReciprocal(float value)
    {
        _timeUnitReciprocal = value;
        GameUIManager.TimeUnit = value;
    }

    public Dictionary<uint, Player> Players = new Dictionary<uint, Player>();
    public Dictionary<uint, Egg> Eggs = new Dictionary<uint, Egg>();
    public Dictionary<string, Team> Teams = new Dictionary<string, Team>();
    public GameMap Map;
    public GameUIManager GameUIManager;
    [SerializeField]
    private LoadingScreen _loadingScreen;
    private float _endScreenDuration = 4.0f;
    private float _gameEndTime = 0.0f;
    private bool _gameEnded;
    private bool _gameInterrupted;

    public void InitMap(Vector2 mapSize)
    {
        Map = new GameMap(mapSize);
        _loadingScreen.Hide();
        GameUIManager.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _loadingScreen.ShowConnectingMessage();
        GameUIManager.gameObject.SetActive(false);
        ServerCommandHandlers.Init();
        /*InitMap(new Vector2(5, 5));
        Teams["Team A"] = new Team(GameManager.Instance.Teams.Count, "Team A");
        Teams["Team B"] = new Team(GameManager.Instance.Teams.Count, "Team B");
        Teams["Team C"] = new Team(GameManager.Instance.Teams.Count, "Team C");
        Teams["Team D"] = new Team(GameManager.Instance.Teams.Count, "Team D");
        AddPlayer(0, "Team A", 1, new Vector2(0, 0), (Player.Orientation.Left));
        Players[0].Level = 4;*/
    }

    public void Connected()
    {
        _loadingScreen.ShowMapLoadingMessage();
    }

    private void Update()
    {
        if (_gameInterrupted)
            GoBackToMenu();
        if (_gameEnded && Time.time > _gameEndTime + _endScreenDuration)
            GoBackToMenu();
        else
            ServerCommandHandlers.zappy_sync_poll();
    }

    public void AddPlayer(uint id, string name, uint level, Vector2 position, Player.Orientation direction)
    {
        GameObject instance = GameObject.Instantiate(Resources.Load("GameObjects/Player/Prefab/Player")) as GameObject;
        Player newPlayer = instance.GetComponent<Player>();
        if (newPlayer != null)
        {
            if (!Teams.ContainsKey(name))
                GameManager.Instance.Teams[name] = new Team(GameManager.Instance.Teams.Count, name);
            newPlayer.Init(id, Teams[name], level, position, direction);
            Players[id] = newPlayer;
        }
        GameUIManager.AddPlayer(name);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void InterruptGame()
    {
        _gameInterrupted = true;
    }

    public void EndGame(string WinningTeamName)
    {
        Debug.Log("Team " + WinningTeamName + " has won !");
        _gameEndTime = Time.time;
        _gameEnded = true;
    }
}
