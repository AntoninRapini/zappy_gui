using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Egg : MonoBehaviour {

    // Id
    private uint _id;
    private Team _team;
    private Vector2 _position;
    private SkinnedMeshRenderer _meshRenderer;
    private float _lifeTime = 300.0f;
    private float _spawnTime;


    public void Init(uint id, Team team, Vector2 position)
    {
        _id = id;
        _team = team;
        _position = position;
        transform.position = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
    }

    private void Start()
    {
        _spawnTime = Time.time;
        _meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (_meshRenderer != null)
        {
                int skinId = _team.Id % Player.Skins.Count;
                Material mat = Resources.Load("GameObjects/Egg/Materials/Egg_" + Player.Skins[skinId]) as Material;
                _meshRenderer.sharedMaterials = new Material[] { mat };
        }
    }

    private void Update()
    {
        if (Time.time > _spawnTime + (_lifeTime / GameManager.Instance.TimeUnitReciprocal))
        {
            GameManager.Instance.Eggs.Remove(_id);
            Destroy(gameObject);
        }
    }

    public void Hatch()
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("GameObjects/Egg/Prefab/HatchedEgg") as GameObject);
        obj.GetComponent<HatchedEgg>().Init(_team, _position);
        GameManager.Instance.Eggs.Remove(_id);
        Destroy(gameObject);
    }
}
