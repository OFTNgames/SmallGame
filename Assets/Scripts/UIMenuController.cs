using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIMenuController : MonoBehaviour
{
    private List<MyUI> _myUIList;
    private MyUI _gameplayUI;
    private MyUI _lastActiveUI;
    private MyUI _previousUI;

    private void Start()
    {
        _myUIList = GetComponentsInChildren<MyUI>().ToList();
        _myUIList.ForEach(x => x.gameObject.SetActive(false));
        _gameplayUI = _myUIList.Find(x => x.menuType == UIType.GAMEUI);
        SwitchUI(UIType.MAINMENU);
    }

    private void OnEnable()
    {
        UISwitcher.ChangeActiveUI += UISwitcher_ChangeActiveUI;
        LevelController.setActiveUI += LevelController_setActiveUI;
        LevelController.LevelEnd += LevelController_LevelEnd;
        LevelController.PauseGame += LevelController_PauseGame;
    }
    private void OnDisable()
    {
        UISwitcher.ChangeActiveUI -= UISwitcher_ChangeActiveUI;
        LevelController.setActiveUI -= LevelController_setActiveUI;
        LevelController.LevelEnd -= LevelController_LevelEnd;
        LevelController.PauseGame -= LevelController_PauseGame;
    }
    
    public void SwitchUI(UIType type)
    {
        if (_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
        }

        if (type == UIType.NULL)
            return;

        MyUI desiredUI = _myUIList.Find(x => x.menuType == type);
        
        if(desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _previousUI = _lastActiveUI;
            _lastActiveUI = desiredUI;
        }
        else { Debug.LogWarning("Not found"); }
    }

    public void DecativateUI()
    { 
        if (_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
            _lastActiveUI = null;
            _previousUI = null;

        }
    }

    public void GoToPreviousUI()
    {
        if (_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
            _previousUI.gameObject.SetActive(true);
            _lastActiveUI = _previousUI;
        }
    }


    private void UISwitcher_ChangeActiveUI(UIType arg1, UIChange arg2)
    {
        switch (arg2)
        {
            case UIChange.SWITCH:
                SwitchUI(arg1);
                break;
            case UIChange.DEACTIVATE:
                DecativateUI();
                break;
            case UIChange.PREVIOUS:
                GoToPreviousUI();
                break;
        }
    }


    private void LevelController_LevelEnd(bool won)
    {
        if(won)
            SwitchUI(UIType.WINMENU);
    }

    private void LevelController_setActiveUI(bool state)
    {
         _gameplayUI.gameObject.SetActive(state);
    }
    
    private void LevelController_PauseGame(bool pauseState)
    {
        if(pauseState)
        {
            SwitchUI(UIType.PAUSEMENU);
        }
        else
        {
            SwitchUI(UIType.NULL);
        }
    }
}
