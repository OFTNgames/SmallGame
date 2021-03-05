using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptableEventChannel _eventChannel;
    [SerializeField] private CanvasGroup _faderCanvasGroup;
    [SerializeField] private float _fadeDuration;

    private string _currentActiveSceneName;

    private bool isFading;

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
        StartCoroutine(Fade(0));
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

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        _faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(_faderCanvasGroup.alpha - finalAlpha)/ _fadeDuration;

        while(!Mathf.Approximately(_faderCanvasGroup.alpha, finalAlpha))
        {
            _faderCanvasGroup.alpha = Mathf.MoveTowards(_faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
        _faderCanvasGroup.blocksRaycasts = false;
    }
}
