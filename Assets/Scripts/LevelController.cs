using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    //EVENTS
    public static event System.Action<int, bool> CountDownEvent = delegate { };
    public static event System.Action<LevelController> CurrentActiveLevelController = delegate { };
    public static event System.Action<bool> LevelEnd = delegate { };
    public static event System.Action<bool> setActiveUI = delegate { };
    public static event System.Action<bool> PauseGame = delegate { };

    [HeaderAttribute("Need To Assign")]
    [SerializeField] private string _currentLevelName;
    [SerializeField] private int _levelIndex;
    [SerializeField] private int startCountdown = 3;
    [SerializeField] private ScriptableEventChannel _scriptableEvent;

    //PUBLIC
    [HideInInspector] public System.TimeSpan timePlaying;
    [HideInInspector] public int bestNumberOfAttempts;
    [HideInInspector] public int numberOfAttempts;
    [HideInInspector] public float bestTime { get; private set; }
    
    private Level _level;
    
    private float _time;

    private string _bestTimeIndex;
    private string _currentNumberOfAttemptsIndex;
    private string _bestNumberOfAttemptsIndex;

    private bool _hasWon;
    private bool _haslost;
    private bool _isPlaying;
    private bool _pause;

    private void OnEnable()
    {
        ResumeEvent.ResumeGame += ResumeEvent_ResumeGame;
        Player.PlayerDeath += Player_PlayerDeath;
        Door.ExitDoorReached += Door_ExitDoorReached;
        setActiveUI?.Invoke(true);
    }
    private void OnDisable()
    {
        ResumeEvent.ResumeGame -= ResumeEvent_ResumeGame;
        Player.PlayerDeath -= Player_PlayerDeath;
        Door.ExitDoorReached -= Door_ExitDoorReached;
        setActiveUI?.Invoke(false);
    }
    void Start()
    {
        _bestNumberOfAttemptsIndex = _currentLevelName + "bestAttempts";
        _currentNumberOfAttemptsIndex = _currentLevelName + "currentAttempts";
        _bestTimeIndex = _currentLevelName + "bestTime";
        
        bestTime = PlayerPrefs.GetFloat(_bestTimeIndex);
        bestNumberOfAttempts = PlayerPrefs.GetInt(_bestNumberOfAttemptsIndex);
        numberOfAttempts = PlayerPrefs.GetInt(_currentNumberOfAttemptsIndex);

        CurrentActiveLevelController?.Invoke(this);

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
            //Time.timeScale = 1f;
            _time += Time.deltaTime;
            timePlaying = System.TimeSpan.FromSeconds(_time);
        }
        else
        {
            //Time.timeScale = 0;
        }

        if(!_haslost && !_hasWon)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!_pause && _isPlaying)
                {
                    _pause = true;
                    _isPlaying = false;
                    PauseGame?.Invoke(_pause);
                }
                else
                {
                    _pause = false;
                    _isPlaying = true;
                    PauseGame?.Invoke(_pause);
                }
            }
        }
    }
    private void ResumeEvent_ResumeGame()
    {
        _pause = false;
        _isPlaying = true;
    }

    private void Player_PlayerDeath()
    {
        _isPlaying = false;
        _haslost = true;
        numberOfAttempts++;
        PlayerPrefs.SetInt(_currentNumberOfAttemptsIndex, numberOfAttempts);
        LevelEnd?.Invoke(false);
        _scriptableEvent.ReloadScene();
    }

    private void Door_ExitDoorReached()
    {
        _isPlaying = false;
        _hasWon = true;
        if (numberOfAttempts < bestNumberOfAttempts || bestNumberOfAttempts == 0)
        {
            PlayerPrefs.SetInt(_bestNumberOfAttemptsIndex, numberOfAttempts);
        }
        if (_time < bestTime || bestTime == 0f)
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
