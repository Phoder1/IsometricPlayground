using UnityEngine;

namespace Assets.TimeEvents {
    public abstract class TimeEventAbst {
        public readonly float eventTriggerTime;
        public readonly TileInterface triggeringTile;
        public abstract void TriggerTimedEvent();

        public TimeEventAbst(TileInterface tile, float triggerTime) {
            eventTriggerTime = triggerTime;
            triggeringTile = tile;
        }
    }

    public class PurpleEvent : TimeEventAbst {
        public PurpleEvent(TileInterface tile, float triggerTime) : base(tile, triggerTime) {
        }

        public override void TriggerTimedEvent() {
            throw new System.NotImplementedException();
        }
    }
}
