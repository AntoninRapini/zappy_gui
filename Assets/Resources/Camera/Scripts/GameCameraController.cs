using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour {

    // Use this for initialization
    Bounds _bounds;
    [SerializeField]
    private float _moveSpeed;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void UpdateBounds(Vector3 terrainSize)
    {
        Vector3 center = new Vector3(terrainSize.x / 2, 50, terrainSize.z / 2);
        Vector3 size = new Vector3(terrainSize.x + 10, 60, terrainSize.z + 40);
        _bounds = new Bounds(center, size);
    }

    public void Update()
    {
        Vector3 newPosition = GetMovement() * _moveSpeed * Time.deltaTime;
        if (newPosition != Vector3.zero && _bounds.Contains(transform.position + newPosition))
            transform.position += newPosition;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    if (player != null)
                    {
                        player.GetFocused();
                    }
                }
            }
        }
    }

    public Vector3 GetMovement()
    {
        int verticalMovement = (Input.GetKey(KeyCode.Space) ? 1 : 0) - (Input.GetKey(KeyCode.C) ? 1 : 0);
        return new Vector3(Input.GetAxisRaw("Horizontal"), verticalMovement, Input.GetAxisRaw("Vertical"));
    }

    
}
