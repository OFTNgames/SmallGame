using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EventChannel")]
public class ScriptableEventChannel : ScriptableObject
{
    public UnityEngine.Events.UnityAction<string> ScriptableEvent;
    [SerializeField] private string _eventName;
    public void RaiseEvent(string eventString)
    {
       ScriptableEvent?.Invoke(eventString);
    }
}
