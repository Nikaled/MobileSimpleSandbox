using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneUpButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;
    public bool isPressedThisFrame;
    public float TranslatingFloat;
    public event Action<float> ActionOnHold;
    public static PlaneUpButton instance;

    private void Start()
    {
        instance = this;
    }
    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            ActionOnHold?.Invoke(TranslatingFloat);
        }
        isPressedThisFrame = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
        isPressedThisFrame = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        ActionOnHold?.Invoke(0);
    }
}

