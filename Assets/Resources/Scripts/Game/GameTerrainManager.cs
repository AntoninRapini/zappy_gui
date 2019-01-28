using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTerrainManager : MonoBehaviour {

    GameObject _terrainObject;
    GameObject _waterObject;
    Terrain _terrain;

    public GameTerrainManager(Vector2 size)
    {
        _terrainObject = GameObject.Instantiate(Resources.Load("Environment/GameTerrainObject") as GameObject);
        _terrain = _terrainObject.GetComponent<Terrain>();
        _terrain.terrainData.size = new Vector3((size.x * 12), 600, (size.y * 12));
        _terrain.detailObjectDistance = 500.0f;
        _waterObject = GameObject.Instantiate(Resources.Load("Environment/Water") as GameObject);
        _waterObject.transform.localScale = new Vector3(10, 1, 10);
        _waterObject.transform.position = new Vector3(_terrain.terrainData.size.x / 2, 9.2f, _terrain.terrainData.size.z / 2);
        Camera.main.GetComponent<GameCameraController>().UpdateBounds(_terrain.terrainData.size);
        Camera.main.transform.position = new Vector3(_terrain.terrainData.size.x / 2, 40, -10);
    }
}
