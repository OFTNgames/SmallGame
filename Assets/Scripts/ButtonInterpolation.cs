using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonInterpolation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        rect.DOComplete();
        rect.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.2f, .3f).SetEase(Ease.OutQuad).SetUpdate(true);
        //Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetUpdate(true);
        //Debug.Log("Exit");
    }
}
