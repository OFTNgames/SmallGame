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
        _eventChannel.ScriptableLoadNextLevel += NextLevel;
    }
    private void OnDisable()
    {
        _eventChannel.ScriptableEvent -= SceneChange;
        _eventChannel.ScriptableReloadScene -= ReloadLevel;
        _eventChannel.ScriptableLoadNextLevel -= NextLevel;
    }

    void Start()
    {
        _faderCanvasGroup.alpha = 1f;
        StartCoroutine(LoadSceneAndSetActive("MainMenu"));
        StartCoroutine(Fade(0));
    }

    private void SceneChange(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScenes(sceneName));
    }

    private void NextLevel()
    {
        StartCoroutine(FadeAndSwitchScenes(SceneUtilityEx.GetNextSceneName()));
    }

    private void ReloadLevel(float wait)
    {
        StartCoroutine(ReloadScenes(wait));
    }

    private IEnumerator ReloadScenes(float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
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

public static class SceneUtilityEx
{
    public static string GetNextSceneName()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            return GetSceneNameByBuildIndex(nextSceneIndex);
        }
        //return string.Empty;
        return "MainMenu";
    }

    public static string GetSceneNameByBuildIndex(int buildIndex)
    {
        return GetSceneNameFromScenePath(SceneUtility.GetScenePathByBuildIndex(buildIndex));
    }

    private static string GetSceneNameFromScenePath(string scenePath)
    {
        var sceneNameStart = scenePath.LastIndexOf("/", System.StringComparison.Ordinal) + 1;
        var sceneNameEnd = scenePath.LastIndexOf(".", System.StringComparison.Ordinal);
        var sceneNameLength = sceneNameEnd - sceneNameStart;
        return scenePath.Substring(sceneNameStart, sceneNameLength);
    }
}