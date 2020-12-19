using Assets.TimeEvents;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles : MonoBehaviour
{
    readonly TileBase purpleTilebase;

    public static Tiles _instance;
    private void Awake() {
        if (_instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }
    public PurpleTile purpleTile => new PurpleTile(purpleTilebase, null);
}



public abstract class TileInterface
{
    public readonly TileBase tile;
    public readonly TimeEventAbst timeEvent;
    public readonly Vector3Int gridPosition;
    internal TileInterface(TileBase _tile, TimeEventAbst _timeEvent) {
        tile = _tile;
        timeEvent = _timeEvent;
        TimeManager._instance.AddEvent(timeEvent);
    }
    public virtual void CancelTimeEvent() => TimeManager._instance.RemoveEvent(timeEvent);
    public virtual void OnClickInteraction() { }
    public virtual void SetTile(Vector2Int gridPosition) { }
    public virtual void RemoveTile(Vector2Int gridPosition) { }

}



public class PurpleTile : TileInterface
{
    public PurpleTile(TileBase _tile, TimeEventAbst _timeEvent) : base(_tile, _timeEvent) {

    }
}
