using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EventChannel")]
public class ScriptableEventChannel : ScriptableObject
{
    public UnityEngine.Events.UnityAction<string> ScriptableEvent;
    public UnityEngine.Events.UnityAction ScriptableReloadScene;
    public UnityEngine.Events.UnityAction ScriptableOptionMenu;
    public UnityEngine.Events.UnityAction ScriptableLoadNextLevel;
    public UnityEngine.Events.UnityAction<float, float> ShakeTheCamera;
    [SerializeField] private string _eventName;
    public void RaiseEvent(string eventString)
    {
       ScriptableEvent?.Invoke(eventString);
    }

    public void ReloadScene()
    {
        ScriptableReloadScene?.Invoke();
    }

    public void OptionsMenu()
    {
        ScriptableOptionMenu?.Invoke();
    }

    public void NextLevel()
    {
        ScriptableLoadNextLevel?.Invoke();
    }

    public void ShakeCamera(float d, float m)
    {
        ShakeTheCamera?.Invoke(d, m);
    }
}
