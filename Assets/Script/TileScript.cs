using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    BuildingTypes buildingType;

    [SerializeField]
    Vector2Int position;
    public Vector2Int GetSetPosition {
        set {
            position = value;
            MoveToPosition();
        }
        get => position;
    }

    private GridManager gridManagerRef;

    private void Start() {
        gridManagerRef = GridManager._instance;
        MoveToPosition();
    }

    private void MoveToPosition() {
        Vector3 realPosition = gridManagerRef.GridToWorldPosition((Vector3Int)position + new Vector3Int(1, 1, 0));
        realPosition.z = 2;
        transform.position = realPosition;
    }
}
