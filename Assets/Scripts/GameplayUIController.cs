using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private Image _gravityBar;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _numberOfAttemptsText;
    [SerializeField] private TextMeshProUGUI _fewestNumberOfAttemptsText;
    private LevelController _currentLevelController;

    private void OnEnable()
    {
        PlayerGravityControl.GravityAmount += UpdateGravityBar;
        LevelController.CurrentActiveLevelController += LevelController_CurrentActiveLevelController;
    }

    private void OnDisable()
    {
        PlayerGravityControl.GravityAmount -= UpdateGravityBar;
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
        _numberOfAttemptsText.text = "Attempts to Clear: " + _currentLevelController.numberOfAttempts.ToString();

        if (_currentLevelController.bestNumberOfAttempts == 0)
            _fewestNumberOfAttemptsText.text = "Fewest Attempts: -";
        else
            _fewestNumberOfAttemptsText.text = "Fewest Attempts: " + _currentLevelController.bestNumberOfAttempts.ToString();

        if (_currentLevelController.bestTime == 0f)
            _bestTimeText.text = "Best Time: --:--.--";
        else
            _bestTimeText.text = "Best Time: " + System.TimeSpan.FromSeconds(_currentLevelController.bestTime).ToString("mm':'ss'.'ff");
    }

    private void UpdateGravityBar(float current, float max)
    {
        _gravityBar.fillAmount = current / max;
    }
}
