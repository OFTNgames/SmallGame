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

    private bool _isFading;

    private void OnEnable()
    {
        _eventChannel.ScriptableEvent += SceneChange;
        _eventChannel.ScriptableReloadScene += ReloadLevel;
    }
    private void OnDisable()
    {
        _eventChannel.ScriptableEvent -= SceneChange;
        _eventChannel.ScriptableReloadScene -= ReloadLevel;
    }

    void Start()
    {
        _faderCanvasGroup.alpha = 1f;
        StartCoroutine(LoadSceneAndSetActive("2MainMenu"));
        StartCoroutine(Fade(0));
    }

    private void SceneChange(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScenes(sceneName));
    }

    private void ReloadLevel()
    {
        Debug.Log("here");
        StartCoroutine(ReloadScenes());
    }

    private IEnumerator ReloadScenes()
    {
        Debug.Log("here2");
        yield return StartCoroutine(Fade(1f));
        //beforeSceneUnload
        string name = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(name));
        //afterSceneLoad
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));
        //beforeSceneUnload
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        //afterSceneLoad
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _currentActiveSceneName = sceneName;
        Scene newlyLoadedScene = SceneManager.GetSceneByName(_currentActiveSceneName);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        _isFading = true;
        _faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(_faderCanvasGroup.alpha - finalAlpha)/ _fadeDuration;

        while(!Mathf.Approximately(_faderCanvasGroup.alpha, finalAlpha))
        {
            _faderCanvasGroup.alpha = Mathf.MoveTowards(_faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        _isFading = false;
        _faderCanvasGroup.blocksRaycasts = false;
    }
}
