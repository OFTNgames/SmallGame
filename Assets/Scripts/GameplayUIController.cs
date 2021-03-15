using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIController:MonoBehaviour
{
    [SerializeField] private Image _gravityBar;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _numberOfAttemptsText;
    private LevelController _currentLevelController;

    private void OnEnable()
    {
        Player.GravityAmount += UpdateGravityBar;
        LevelController.CurrentActiveLevelController += LevelController_CurrentActiveLevelController;
    }

    private void OnDisable()
    {
        Player.GravityAmount -= UpdateGravityBar;
        LevelController.CurrentActiveLevelController -= LevelController_CurrentActiveLevelController;
    }

    private void Update()
    {
        if (_currentLevelController != null)
        {
            _timeText.text = "Time: " + _currentLevelController.timePlaying.ToString("mm':'ss'.'ff");
        }
    }

    private void LevelController_CurrentActiveLevelController(LevelController obj)
    {
        _currentLevelController = obj;
        _numberOfAttemptsText.text = "Attempts to Clear: " + _currentLevelController.numberOfAttempts.ToString(); ;
    }

    private void UpdateGravityBar(float current, float max)
    {
        _gravityBar.fillAmount = current / max;
    }
}
