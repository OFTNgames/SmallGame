using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClickSound : MonoBehaviour, IPointerEnterHandler
{
    public static event System.Action PlayClickSound = delegate { };
    public static event System.Action PlayHooverSound = delegate { };

    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
    }

    public void Click()
    {
        PlayClickSound?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHooverSound?.Invoke();
    }
}
