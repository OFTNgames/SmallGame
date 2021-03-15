using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private ScriptableEventChannel _eventChannel;


    private List<MyUI> _myUIPanelsList;
    private MyUI _gameplayUI;
    private MyUI _lastActiveUI;

    private void Start()
    {
        _myUIPanelsList = GetComponentsInChildren<MyUI>().ToList();
        _myUIPanelsList.ForEach(x => x.gameObject.SetActive(false));
        _gameplayUI = _myUIPanelsList.Find(x => x.menuType == MenuType.GAMEUI);
        SwitchCanvas(MenuType.MAINMENU);
    }

    public void SwitchCanvas(MenuType type)
    {
        if (_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
        }

        if (type == MenuType.NULL)
            return;

        MyUI desiredUI = _myUIPanelsList.Find(x => x.menuType == type);
        
        if(desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _lastActiveUI = desiredUI;
        }
        else { Debug.LogWarning("Not found"); }
    }

    //public void DecativateCanvas()
    //{ 
    //    if (_lastActiveUI != null)
    //    {
    //        _lastActiveUI.gameObject.SetActive(false);
    //    }
    //}

    private void OnEnable()
    {
        LevelController.setActiveUI += LevelController_setActiveUI;
        LevelController.LevelEnd += LevelController_LevelEnd;
        LevelController.PauseGame += LevelController_PauseGame;
    }

    private void OnDisable()
    {
        LevelController.setActiveUI -= LevelController_setActiveUI;
        LevelController.LevelEnd -= LevelController_LevelEnd;
        LevelController.PauseGame -= LevelController_PauseGame;
    }

    private void LevelController_LevelEnd(bool won)
    {
        if(won)
            SwitchCanvas(MenuType.WINMENU);
    }

    private void LevelController_setActiveUI(bool state)
    {
         _gameplayUI.gameObject.SetActive(state);
    }
    
    private void LevelController_PauseGame(bool pauseState)
    {
        if(pauseState)
        {
            SwitchCanvas(MenuType.PAUSEMENU);
        }
        else
        {
            SwitchCanvas(MenuType.NULL);
        }
    }
}
