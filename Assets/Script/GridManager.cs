using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    [SerializeField]
    Tilemap floor;
    [SerializeField]
    Tilemap elevated;
    [SerializeField]
    Tilemap buildings;
    [SerializeField]
    Tilemap overlay;

    public static GridManager _instance;

    private void Awake() {
        if(_instance != null) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

    public Vector3 GridToWorldPosition(Vector3Int position) {
        return grid.CellToWorld(position);
    }
}
