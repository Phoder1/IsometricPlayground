using Assets.TimeEvents;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    LinkedList<TimeEventAbst> timedEventsList;

    public static TimeManager _instance;
    private void Awake() {
        if( _instance == null) {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }else {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start() { 
        Debug.Log("Current time: " + Time.deltaTime);
        timedEventsList = new LinkedList<TimeEventAbst>();
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

    public void AddEvent(TimeEventAbst timedEvent) {
        float eventTriggerTime = timedEvent.eventTriggerTime;
        if (timedEventsList.Count > 0 && eventTriggerTime > timedEventsList.First.Value.eventTriggerTime) {
            LinkedListNode<TimeEventAbst> currentNode = timedEventsList.Last;
            while (currentNode != timedEventsList.First && eventTriggerTime < currentNode.Value.eventTriggerTime) {
                currentNode = currentNode.Previous;
            }
            timedEventsList.AddAfter(currentNode, timedEvent);
        }
        else {
            timedEventsList.AddFirst(timedEvent);
        }
    }

    public void RemoveEvent(TimeEventAbst timedEvent) {
        LinkedListNode<TimeEventAbst> currentNode = timedEventsList.First;
        while (currentNode.Value != timedEvent && currentNode != timedEventsList.Last) {
            currentNode = currentNode.Next;
        }
        if(currentNode.Value == timedEvent) {
            timedEventsList.Remove(currentNode);
        }
    }
}
