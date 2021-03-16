using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private UISwitcher _uISwitcher;
    void Start()
    {
        _uISwitcher = GetComponent<UISwitcher>();
        _uISwitcher.OnButtonClicked();
    }
}
