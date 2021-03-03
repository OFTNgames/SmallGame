using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static event System.Action<int, bool> CountDownEvent = delegate { };
    public static event System.Action<LevelController> CurrentActiveLevelController = delegate { };
    public static event System.Action<bool> LevelEnd = delegate { };
    public static event System.Action<bool> setActiveUI = delegate { };

    [SerializeField] private string _currentLevelName;
    private string _bestTimeIndex;
    private string _currentNumberOfAttemptsIndex;
    private string _bestNumberOfAttemptsIndex;

    public int startCountdown = 3;
    public System.TimeSpan bestTime;
    public System.TimeSpan timePlaying;
    public int bestNumberOfAttempts;
    public int numberOfAttempts;
    
    private float _time;
    private float _bestTime;


    private bool _hasWon;
    private bool _haslost;
    private bool _isPlaying;

    private void OnEnable()
    {
        Player.PlayerDeath += Player_PlayerDeath;
        Door.ExitDoorReached += Door_ExitDoorReached;
        setActiveUI?.Invoke(true);
    }

    private void OnDisable()
    {
        Player.PlayerDeath -= Player_PlayerDeath;
        Door.ExitDoorReached -= Door_ExitDoorReached;
        setActiveUI?.Invoke(false);
    }
    private void Awake()
    {
        
    }

    void Start()
    {
        _bestNumberOfAttemptsIndex = _currentLevelName + "bestAttempts";
        _currentNumberOfAttemptsIndex = _currentLevelName + "currentAttempts";
        _bestTimeIndex = _currentLevelName + "bestTime";
        
        _bestTime = PlayerPrefs.GetFloat(_bestTimeIndex);
        bestTime = System.TimeSpan.FromSeconds(_bestTime);

        bestNumberOfAttempts = PlayerPrefs.GetInt(_bestNumberOfAttemptsIndex);
        numberOfAttempts = PlayerPrefs.GetInt(_currentNumberOfAttemptsIndex);

        CurrentActiveLevelController?.Invoke(this);
        Debug.Log("Sent");

        _time = 0.0f;
        _hasWon = false;
        _haslost = false;
        _isPlaying = false;

        StartCoroutine(Countdown());
    }

    void Update()
    {
        if(_isPlaying)
        {
            _time += Time.deltaTime;
            timePlaying = System.TimeSpan.FromSeconds(_time);
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private void Player_PlayerDeath()
    {
        _isPlaying = false;
        _haslost = true;
        numberOfAttempts++;
        PlayerPrefs.SetInt(_currentNumberOfAttemptsIndex, numberOfAttempts);
        LevelEnd?.Invoke(false);
    }

    private void Door_ExitDoorReached()
    {
        _isPlaying = false;
        _hasWon = true;
        if (numberOfAttempts < bestNumberOfAttempts)
        {
            PlayerPrefs.SetInt(_bestNumberOfAttemptsIndex, numberOfAttempts);
        }
        if(_time < _bestTime)
        {
            PlayerPrefs.SetFloat(_bestTimeIndex, _time);
        }
        LevelEnd?.Invoke(true);
    }

    private IEnumerator Countdown()
    {
        while (startCountdown > 0)
        {
            CountDownEvent?.Invoke(startCountdown, false);
            yield return new WaitForSecondsRealtime(1f);
            startCountdown--;
        }
        CountDownEvent?.Invoke(startCountdown, true);
        _isPlaying = true;
        Time.timeScale = 1f;
    }
}
