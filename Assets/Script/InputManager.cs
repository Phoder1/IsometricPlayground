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
        tilemap.SetTile(Vector3Int.zero, purple);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
                if(hit.collider != null) {
                    Debug.Log("HIT!");
                }
            }
            else {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                Vector3Int position = tilemap.WorldToCell(mousePos);
                if (tilemap.GetTile(position) != purple) {
                    Debug.Log("Tile!");
                    tilemap.SetTile(position, purple);
                }
                GridManager._instance.SetTileAtPos((Vector2Int)position, BuildingTypes.Purple);
            }
        }
    }
}
