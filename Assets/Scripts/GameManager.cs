using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptableEventChannel _eventChannel;
    private string _currentActiveSceneName;

    private void OnEnable()
    {
        _eventChannel.ScriptableEvent += SceneChange;     
    }
    private void OnDisable()
    {
        _eventChannel.ScriptableEvent -= SceneChange;
    }

    void Start()
    {
        SceneManager.LoadSceneAsync("2MainMenu", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("3UI", LoadSceneMode.Additive);
        _currentActiveSceneName = "2MainMenu";
    }

    private void SceneChange(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_currentActiveSceneName);
        _currentActiveSceneName = sceneName;
    }
}
