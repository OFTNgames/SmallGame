using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeEvent : MonoBehaviour
{
    public static event System.Action ResumeGame = delegate { };

    private Button _menuButton;

    private void Start()
    {
        _menuButton = GetComponent<Button>();
        _menuButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ResumeGame?.Invoke();
    }
}
