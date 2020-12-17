using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    LinkedList<TimedEventAbst> timedEventsList;
    // Start is called before the first frame update
    void Start() { 
        Debug.Log("Current time: " + Time.deltaTime);
        timedEventsList = new LinkedList<TimedEventAbst>();
        DebugEventsOrder();
    }

    // Update is called once per frame
    void Update() {
        if (timedEventsList.Count > 0) {
            while (timedEventsList.First.Value.eventTriggerTime <= Time.time) {
                timedEventsList.First.Value.TriggerTimedEvent();
                timedEventsList.RemoveFirst();
                if(timedEventsList.Count == 0) { break; }
            }
        }
    }

    public void AddEvent(TimedEventAbst timedEvent) {
        float eventTriggerTime = timedEvent.eventTriggerTime;
        if (timedEventsList.Count > 0 && eventTriggerTime > timedEventsList.First.Value.eventTriggerTime) {
            LinkedListNode<TimedEventAbst> currentNode = timedEventsList.Last;
            while (currentNode != timedEventsList.First && eventTriggerTime < currentNode.Value.eventTriggerTime) {
                currentNode = currentNode.Previous;
            }
            timedEventsList.AddAfter(currentNode, timedEvent);
        }
        else {
            timedEventsList.AddFirst(timedEvent);
        }
    }


    public void DebugEventsOrder() {
        for (int i = 0; i < 20; i++) {
            float time = Time.time + Random.Range(5f, 30f);
            AddEvent(new DebugEvent() { eventTriggerTime = time });
        }
        LinkedListNode<TimedEventAbst> currentEvent = timedEventsList.First;
        for (int i = 0; i < timedEventsList.Count; i++) {
            Debug.Log("Event " + i + " time: " + currentEvent.Value.eventTriggerTime);
            currentEvent = currentEvent.Next;
        }
    }
}

public abstract class TimedEventAbst {
    public float eventTriggerTime;
    public abstract void TriggerTimedEvent();
}

public class DebugEvent : TimedEventAbst {
    public override void TriggerTimedEvent() {
        Debug.Log("Triggered!");
    }
}
