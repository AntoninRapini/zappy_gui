using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    #region Direction
    public enum Orientation { Up, Right, Down, Left }
    static Dictionary<Orientation, Vector2> _directions = new Dictionary<Orientation, Vector2>
    {
        {Orientation.Up, new Vector2(0, 1) },
        {Orientation.Right, new Vector2(1, 0) },
        {Orientation.Down, new Vector2(0, -1) },
        {Orientation.Left, new Vector2(-1, 0) },
    };

    #endregion
    private enum PlayerStates {Idle, Move, Turn, Look, GetInventory, BroadCastText, LayEgg, EjectPlayers, GetEjected, TakeObject, DropObject, Incantate, Die}
    static Dictionary<PlayerStates, float> ActionDuration = new Dictionary<PlayerStates, float>
    {
        {PlayerStates.GetEjected, 4.0f},
        {PlayerStates.Move, 7.0f},
        {PlayerStates.Turn, 7.0f},
        {PlayerStates.Look, 7.0f},
        {PlayerStates.GetInventory, 1.0f},
        {PlayerStates.BroadCastText, 7.0f},
        {PlayerStates.LayEgg, 42.0f},
        {PlayerStates.EjectPlayers, 7.0f},
        {PlayerStates.TakeObject, 7.0f},
        {PlayerStates.DropObject, 7.0f},
        {PlayerStates.Incantate, 300.0f},
    };

    public static Dictionary<int, string> Skins = new Dictionary<int, string>()
    {
        {0, "bronze"},
        {1, "blue"},
        {2, "green"},
        {3, "stormred"},
        {4, "stormpurple"},
        {5, "palestorm"},
        {6, "frost"},
        {7, "blood"},
        {8, "stormblue"},
        {9, "stormgreen"}
    };
    private const int StartingLife = 10;

    // Position
    private Vector2 _position;
    private Vector2 _offset;
    private List<Vector2> _newPositions = new List<Vector2>();
    public Vector2 Position
    {
        get { return _position; }
        set
        {
            _newPositions.Add(value);
            _stateQueue.Add(PlayerStates.Move);
        }
    }

    private List<uint> _eggsToLay = new List<uint>();
    private List<uint> _resourcesToDrop = new List<uint>();
    private List<uint> _resourcesToTake = new List<uint>();
    private List<Vector2> _ejectionPositions = new List<Vector2>();
    private Vector2 _ejectOffset;

    // Direction
    private Orientation _direction;
    private List<Orientation> _newDirections = new List<Orientation>();
    public Orientation Direction
    {
        get { return _direction; }
        set
        {
                _newDirections.Add(value);
                _stateQueue.Add(PlayerStates.Turn);
        }
    }

    private uint _level;
    public uint Level
    {
        get { return _level; }
        set
        {
            _level = value;
            _uiManager.OnLevelChange(_level);
        }
    }

    public bool Focused;
    private PlayerUIManager _uiManager;
    private Material _mat;
    private uint _life;
    public uint Life
    {
        get { return _life; }
        set
        {
            _life = value;
            _uiManager.OnLifeChange(_life);
        }
    }
    public Inventory Inventory { get; set; }
    private bool inAction;
    private Animator _animator;
    private SkinnedMeshRenderer _meshRenderer;
    private List<PlayerStates> _stateQueue = new List<PlayerStates>();
    // Id
    private uint _id;
    private Team _team;

    public void Init(uint id, Team team, uint level, Vector2 position, Orientation direction)
    {
        _uiManager = GetComponent<PlayerUIManager>();
        _animator = GetComponent<Animator>();
        _id = id;
        _team = team;
        _life = 10;
        Level = level;
        _direction = direction;
        _position = position;
        transform.position = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
        transform.rotation = Quaternion.LookRotation(GameMap.MapPositionToWorld(_position + _directions[direction], GameManager.Instance.Map.Size) - GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size));
        _meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (_meshRenderer != null)
        {
            int skinId = _team.Id % Skins.Count;
            _mat = Resources.Load("GameObjects/Player/Materials/dragonwhelp2_" + Skins[skinId]) as Material;
            _meshRenderer.materials = new Material[] { _mat, _mat };
        }
        Inventory = new Inventory(0, 0, 0, 0, 0, 0, 0);
        currentState = PlayerStates.Idle;
    }

    bool ActionOver()
    {
        return (Time.time > _timeEnteredState + (ActionDuration[(PlayerStates)currentState] / GameManager.Instance.TimeUnitReciprocal));
    }

    protected override void LateGlobalSuperUpdate()
    {
        base.LateGlobalSuperUpdate();
        //Debug.Log("Real position : " + _position + " vs computed position : " + GameMap.WorldPositionToMap(transform.position, GameManager.Instance.Map.Size));
    }

    public void LayEgg(uint eggNumber)
    {
        _eggsToLay.Add(eggNumber);
        _stateQueue.Add(PlayerStates.LayEgg);
    }

    public void DropResource(uint ResourceId)
    {
        _resourcesToDrop.Add(ResourceId);
        _stateQueue.Add(PlayerStates.DropObject);
    }

    public void GetResource(uint ResourceId)
    {
        _resourcesToTake.Add(ResourceId);
        _stateQueue.Add(PlayerStates.TakeObject);
    }

    public void Die()
    {
        _stateQueue.Add(PlayerStates.Die);
    }

    public void Eject()
    {
        _stateQueue.Add(PlayerStates.EjectPlayers);
    }

    public void GetEjected(Vector2 direction)
    {
        if ((PlayerStates)currentState != PlayerStates.Move && (PlayerStates)currentState != PlayerStates.GetEjected)
        {
            _ejectionPositions.Add(direction);
            _stateQueue.Add(PlayerStates.GetEjected);
        }
    }

    public void Broadcast(string message)
    {
        _stateQueue.Add(PlayerStates.BroadCastText);
    }

    public void StartIncantation()
    {
        _stateQueue.Add(PlayerStates.Incantate);
    }

    public void StopIncantation(bool result)
    {
        if ((PlayerStates)currentState == PlayerStates.Incantate)
        {
            currentState = PlayerStates.Idle;
        }
    }

    public void GetFocused()
    {
        GameManager.Instance.GameUIManager.ShowPlayer(_id, _team.Name, Inventory.Items[0], Inventory.Items[1], Inventory.Items[2], Inventory.Items[3], Inventory.Items[4], Inventory.Items[5], Inventory.Items[6]);
    }

    #region Idle
    void Idle_EnterState()
    {
        _animator.SetInteger("State", 0);
    }

    void Idle_Update()
    {
       if (_stateQueue.Count != 0)
        {
            currentState = _stateQueue[0];
            _stateQueue.RemoveAt(0);
        }
    }

    void Idle_ExitState()
    {
    }
    #endregion

    #region Get Ejected

    void GetEjected_EnterState()
    {
        if (_ejectionPositions.Count <= 0)
        {
            currentState = PlayerStates.Idle;
            return;
        }
        _animator.SetInteger("State", 4);
        _ejectOffset = _ejectionPositions[0] - _position;
        if (_ejectOffset != _ejectOffset.normalized)
        {
            _ejectOffset = (-_ejectOffset).normalized;
        }
    }

    void GetEjected_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
        if (_ejectionPositions.Count > 0)
        {
            Vector3 start = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
            Vector3 goal = GameMap.MapPositionToWorld(_position + _ejectOffset, GameManager.Instance.Map.Size);
            float percentage = (Time.time - _timeEnteredState) / (ActionDuration[(PlayerStates)currentState] / GameManager.Instance.TimeUnitReciprocal);
            transform.position = Vector3.Lerp(start, goal, percentage);
        }
    }

    void GetEjected_ExitState()
    {
        if (_ejectionPositions.Count > 0)
        {
            _position = _ejectionPositions[0];
            transform.position = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
            _ejectionPositions.RemoveAt(0);
        } 
    }
    #endregion

    #region Move
    void Move_EnterState()
    {
        if (_newPositions.Count <= 0)
        {
            currentState = PlayerStates.Idle;
            return;
        }
        _animator.SetInteger("State", 1);
        _offset = _newPositions[0] - _position;
        if (_offset != _offset.normalized)
        {
            _offset = (-_offset).normalized;
        }
    }

    void Move_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
        if (_newPositions.Count > 0)
        {
            Vector3 start = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
            Vector3 goal = GameMap.MapPositionToWorld(_position + _offset, GameManager.Instance.Map.Size);
            float percentage = (Time.time - _timeEnteredState) / (ActionDuration[(PlayerStates)currentState] / GameManager.Instance.TimeUnitReciprocal);
            transform.position = Vector3.Lerp(start, goal, percentage);
        }
    }

    void Move_ExitState()
    {
        if (_newPositions.Count > 0)
        {
            _position = _newPositions[0];
            transform.position = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
            _newPositions.RemoveAt(0);
        }
    }
    #endregion

    #region Turn
    void Turn_EnterState()
    {
        if (_newDirections.Count <= 0)
        {
            currentState = PlayerStates.Idle;
            return;
        }
        _animator.SetInteger("State", 0);
    }

    void Turn_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
        if (_newDirections.Count > 0)
        {
            Vector3 pos = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
            Quaternion start = Quaternion.LookRotation(GameMap.MapPositionToWorld(_position + _directions[_direction], GameManager.Instance.Map.Size) - pos);
            Quaternion end = Quaternion.LookRotation(GameMap.MapPositionToWorld(_position + _directions[_newDirections[0]], GameManager.Instance.Map.Size) - pos);
            float percentage = (Time.time - _timeEnteredState) / (ActionDuration[(PlayerStates)currentState] / GameManager.Instance.TimeUnitReciprocal);
            transform.rotation = Quaternion.Slerp(start, end, percentage);
        }
    }

    void Turn_ExitState()
    {
        if (_newDirections.Count > 0)
        {
            _direction = _newDirections[0];
            transform.rotation = Quaternion.LookRotation(GameMap.MapPositionToWorld(_position + _directions[_newDirections[0]], GameManager.Instance.Map.Size) - GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size));
            _newDirections.RemoveAt(0);
        }
    }
    #endregion

    #region Look
    void Look_EnterState()
    {
        _animator.SetInteger("State", 2);
    }

    void Look_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void Look_ExitState()
    {
    }
    #endregion

    #region GetInventory
    void GetInventory_EnterState()
    {
        _animator.SetInteger("State", 0);
    }

    void GetInventory_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void GetInventory_ExitState()
    {
    }
    #endregion

    #region BroadCastText
    void BroadCastText_EnterState()
    {
        _animator.SetInteger("State", 3);
    }

    void BroadCastText_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void BroadCastText_ExitState()
    {
    }
    #endregion

    #region LayEgg
    void LayEgg_EnterState()
    {
        if (_eggsToLay.Count <= 0)
        {
            currentState = PlayerStates.Idle;
        }
    }

    void LayEgg_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void LayEgg_ExitState()
    {
        if (_eggsToLay.Count > 0)
        {
            GameObject newEgg = GameObject.Instantiate(Resources.Load("GameObjects/Egg/Prefab/Egg")) as GameObject;
            Egg eggComponent = newEgg.GetComponent<Egg>();
            if (eggComponent != null)
            {
                eggComponent.Init(_eggsToLay[0], _team, _position);
                GameManager.Instance.Eggs[_eggsToLay[0]] = eggComponent;
            }
            _eggsToLay.RemoveAt(0);
        }
    }
    #endregion

    #region EjectPlayers
    void EjectPlayers_EnterState()
    {
        _animator.SetInteger("State", 3);
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void EjectPlayers_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void EjectPlayers_ExitState()
    {
        foreach (KeyValuePair<uint, Player> entry in GameManager.Instance.Players)
        {
            if (entry.Key != _id)
            {
                if (entry.Value._position == _position)
                {
                    Vector2 newPos = _position + _directions[_direction];
                    newPos = new Vector2(newPos.x < 0 ? newPos.x + GameManager.Instance.Map.Size.x : newPos.x % GameManager.Instance.Map.Size.x,
                        newPos.y < 0 ? newPos.y + GameManager.Instance.Map.Size.y : newPos.y % GameManager.Instance.Map.Size.y);
                    entry.Value.GetEjected(newPos);
                }
            }
        }
    }
    #endregion

    #region TakeObject
    void TakeObject_EnterState()
    {
        if (_resourcesToTake.Count <= 0)
        {
            currentState = PlayerStates.Idle;
        }
        _animator.SetInteger("State", 3);
    }

    void TakeObject_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void TakeObject_ExitState()
    {
        if (_resourcesToTake.Count > 0)
        {
            GameManager.Instance.Map.GetItem(_position, _resourcesToTake[0]);
            Inventory.AddItem(1, _resourcesToTake[0]);
            _resourcesToTake.RemoveAt(0);
            if ((int)_id == GameManager.Instance.GameUIManager.FocusedId)
                GetFocused();
        }
    }
    #endregion

    #region SetObjectDown
    void DropObject_EnterState()
    {
        if (_resourcesToDrop.Count <= 0)
        {
            currentState = PlayerStates.Idle;
        }
        _animator.SetInteger("State", 3);
    }

    void DropObject_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void DropObject_ExitState()
    {
        if (_resourcesToDrop.Count > 0)
        {
            Inventory.DropItem(_position, _resourcesToDrop[0]);
            _resourcesToDrop.RemoveAt(0);
            if ((int)_id == GameManager.Instance.GameUIManager.FocusedId)
                GetFocused();
        }
    }
    #endregion

    #region StartIncantation
    void Incantate_EnterState()
    {
        _animator.SetInteger("State", 5);
    }

    void Incantate_Update()
    {
        if (ActionOver())
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void Incantate_ExitState()
    {
    }
    #endregion

    #region Die
    void Die_EnterState()
    {
        _animator.SetInteger("State", 6);
        GameManager.Instance.Players.Remove(_id);
    }

    void Die_Update()
    {
        if (Time.time > _timeEnteredState + 3.0f)
        {
            currentState = PlayerStates.Idle;
            return;
        }
    }

    void Die_ExitState()
    {
        GameManager.Instance.GameUIManager.RemovePlayer(name);
        Destroy(gameObject);
    }
    #endregion
}
