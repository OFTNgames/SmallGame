using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    [SerializeField] private Image _gravityBar;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _numberOfAttemptsText;

    [SerializeField] private GameObject _gameUI;

    private LevelController _currentLevelController; 

    private void OnEnable()
    {
        Debug.Log("Enable");
        Player.GravityAmount += UpdateGravityBar;
        LevelController.CurrentActiveLevelController += LevelController_CurrentActiveLevelController;
        LevelController.setActiveUI += LevelController_setActiveUI;
    }

    private void OnDisable()
    {
        Player.GravityAmount += UpdateGravityBar;
        LevelController.CurrentActiveLevelController += LevelController_CurrentActiveLevelController;
        LevelController.setActiveUI -= LevelController_setActiveUI;
    }
    private void LevelController_setActiveUI(bool state)
    {
        _gameUI.SetActive(state);
    }

    private void Update()
    {
        if(_currentLevelController != null)
        { 
            _timeText.text = "Time: " + _currentLevelController.timePlaying.ToString("mm':'ss'.'ff");
        }
    }

    private void LevelController_CurrentActiveLevelController(LevelController obj)
    {
        Debug.Log("Recieved");
        _currentLevelController = obj;
        _numberOfAttemptsText.text = "Attempts to Clear: " + _currentLevelController.numberOfAttempts.ToString(); ;
    }

    private void UpdateGravityBar(float current, float max)
    {
        _gravityBar.fillAmount = current / max;
    }
}
