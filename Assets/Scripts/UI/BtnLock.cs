using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnLock : Button, IPointerDownHandler
{
    public override void OnPointerDown (PointerEventData eventData) 
	{
		base.OnPointerDown(eventData);
        Cursor.lockState = CursorLockMode.Locked;

        onClick.Invoke();
	}
}
