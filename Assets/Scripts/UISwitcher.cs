using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISwitcher : MonoBehaviour
{
    public static event System.Action<UIType, UIChange> ChangeActiveUI = delegate{};

    public UIChange changeType;
    public UIType desiredCanvasType;

    private Button _menuButton;

    private void Start()
    {
        _menuButton = GetComponent<Button>();
        _menuButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ChangeActiveUI?.Invoke(desiredCanvasType, changeType);
    }
}
