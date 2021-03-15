using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    public MenuType desiredCanvasType;

    UIMenuController _uIMenuController;
    private Button _menuButton;

    private void Start()
    {
        _menuButton = GetComponent<Button>();
        _menuButton.onClick.AddListener(OnButtonClicked);
        _uIMenuController = FindObjectOfType<UIMenuController>();
    }

    private void OnButtonClicked()
    {
        _uIMenuController.SwitchCanvas(desiredCanvasType);
    }
}
