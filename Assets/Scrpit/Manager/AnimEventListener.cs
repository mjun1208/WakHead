using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventListener : MonoBehaviour
{
    [SerializeField] UnityEvent[] events;

    public void EventCall(int index) {
        if (index >= 0 && index < events.Length)
        {
            events[index].Invoke();
        }
    }
}
