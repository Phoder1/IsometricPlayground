using UnityEngine;

public class ObjectTileScript: MonoBehaviour
{

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
        //Vector3 realPosition = gridManagerRef.GridToWorldPosition((Vector3Int)position + new Vector3Int(1, 1, 0));
        Vector3 realPosition = gridManagerRef.GridToWorldPosition(position);
        realPosition.z = 2;
        transform.position = realPosition;
    }
}
