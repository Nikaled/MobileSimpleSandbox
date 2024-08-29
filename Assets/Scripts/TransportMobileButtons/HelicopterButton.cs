using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelicopterButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    public float TranslatingFloat;
    public event Action<float> ActionOnHold;
    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            ActionOnHold?.Invoke(TranslatingFloat);
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        ActionOnHold?.Invoke(0);    
    }
}
