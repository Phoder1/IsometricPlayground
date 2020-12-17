using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BuildingTypes { Purple, Flower, Tree }
public enum TilemapLayers { Floor, Elevated, Buildings, Overlay }
public class GridManager : MonoBehaviour {
    //Debug Variables (Disable when not needed):
    private static List<Vector2Int> chunksDebugList = new List<Vector2Int>();
    private void OnDrawGizmos() {
        foreach (Vector2Int chunkStartCorner in chunksDebugList) {
            Vector2Int maxCorner = chunkStartCorner + Vector2Int.one * 16;
            Vector3 leftCorner = GridToWorldPosition(Vector2Int.RoundToInt(new Vector2(chunkStartCorner.x, maxCorner.y)));
            Vector3 rightCorner = GridToWorldPosition(Vector2Int.RoundToInt(new Vector2(maxCorner.x, chunkStartCorner.y)));
            Vector3 topCorner = GridToWorldPosition(maxCorner);
            Vector3 bottomCorner = GridToWorldPosition(chunkStartCorner);
            //Debug.Log("Origin: Min:" + chunkStartCorner + ", Max:" + maxCorner + ", Real: Bottom:" + bottomCorner + ", Top:" + topCorner + "Left:" + leftCorner + ", Right:" + rightCorner);
            Gizmos.DrawLine(bottomCorner,leftCorner);
            Gizmos.DrawLine(bottomCorner,rightCorner);
            Gizmos.DrawLine(leftCorner, topCorner);
            Gizmos.DrawLine(rightCorner, topCorner);
        }
    }



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

    private ChunksGrid chunksGrid = ChunksGrid._getInstance;
    private void Awake() {
        if (_instance != null) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

    public Vector3 GridToWorldPosition(Vector2Int position) => grid.CellToWorld((Vector3Int)position);
    public BuildingTypes GetTileAtPos(Vector2Int gridPos) => chunksGrid.GetTileAtGridPos(gridPos);
    public void SetTileAtPos(Vector2Int gridPos, BuildingTypes tile) {
        chunksGrid.SetTileAtGridPos(gridPos, tile);

    }

    private class ChunksGrid {

        private const int chunkSize = 16;

        //Chunks Dictionary by chunk cordinates
        private Dictionary<Vector2Int, Chunk> chunksDict;

        //Singleton
        private static ChunksGrid _instance;
        internal static ChunksGrid _getInstance {
            get {
                if (_instance == null) {
                    _instance = new ChunksGrid();
                }
                return _instance;
            }
        }

        //Initializer
        private ChunksGrid() => chunksDict = new Dictionary<Vector2Int, Chunk>();

        internal BuildingTypes GetTileAtGridPos(Vector2Int position)
            => GetChunkAtPos(position).GetTileAtGridPos(position);
        internal void SetTileAtGridPos(Vector2Int position, BuildingTypes tile)
            => GetChunkAtPos(position).SetTileAtGridPos(position, tile);
        private Chunk GetChunkAtPos(Vector2Int position) {
            position = new Vector2Int(Mathf.FloorToInt((float)position.x / chunkSize) * chunkSize, Mathf.FloorToInt((float)position.y / chunkSize) * chunkSize );
            Debug.Log(position);
            Debug.Log(Mathf.FloorToInt(Mathf.Floor(position.x / chunkSize) * chunkSize));
            Chunk chunk;
            if (!chunksDict.TryGetValue(position, out chunk)) {
                chunk = new Chunk(position);
                chunksDict.Add(position, chunk);
            }
            return chunk;
        }
        internal struct Chunk {
            private BuildingTypes[,] chunkArr;
            private Vector2Int gridStartPos;
            private Vector2Int gridEndPos;
            public Chunk(Vector2Int StartPos) {
                gridStartPos = StartPos;
                gridEndPos = gridStartPos + Vector2Int.one * chunkSize;
                chunkArr = new BuildingTypes[chunkSize, chunkSize];
                chunksDebugList.Add( gridStartPos );
                Debug.Log("Min: " + gridStartPos + ", Max: " + gridEndPos);
            }
            internal BuildingTypes GetTileAtGridPos(Vector2Int position)
                => chunkArr[position.x - gridStartPos.x, position.y - gridStartPos.y];

            internal void SetTileAtGridPos(Vector2Int position, BuildingTypes tile) {
                chunkArr[Mathf.Abs( position.x - gridStartPos.x), Mathf.Abs(position.y - gridStartPos.y)] = tile;
            }
                
        }
    }


}
