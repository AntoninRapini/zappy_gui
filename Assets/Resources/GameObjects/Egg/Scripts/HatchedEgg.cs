using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchedEgg : MonoBehaviour {

    // Id
    private Team _team;
    private Vector2 _position;
    private float _hatchTime;
    private const float _disappearTime = 300.0f;
    private SkinnedMeshRenderer _meshRenderer;

    public void Init(Team team, Vector2 position)
    {
        _team = team;
        _position = position;
        transform.position = GameMap.MapPositionToWorld(_position, GameManager.Instance.Map.Size);
    }

    private void Start()
    {
        _hatchTime = Time.time;
        _meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (_meshRenderer != null)
        {
            int skinId = _team.Id % Player.Skins.Count;
            Material mat = Resources.Load("GameObjects/Egg/Materials/Egg_" + Player.Skins[skinId]) as Material;
            _meshRenderer.sharedMaterials = new Material[] { mat };
        }
    }

    void Update()
    {
        if (Time.time > _hatchTime + (_disappearTime / GameManager.Instance.TimeUnitReciprocal))
        {
            Destroy(gameObject);
        }
    }
}
