using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    Grid grid;
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    TileBase purple;
    [SerializeField]
    TileBase black;


    private void Start() {
        Debug.Log(tilemap.GetCellCenterLocal(Vector3Int.zero));
        Debug.Log(grid.GetCellCenterLocal(Vector3Int.zero));
        tilemap.SetTile(Vector3Int.zero, purple);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3Int position = tilemap.WorldToCell(mousePos);
            if (tilemap.HasTile(position) && tilemap.GetTile(position) != purple) {
                Debug.Log("Tile!");
                tilemap.SetTile(position, purple);
            }
        }
    }
}
