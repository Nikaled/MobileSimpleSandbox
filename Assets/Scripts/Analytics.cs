using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    public static Analytics instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    public void SendEvent(string eventStr)
    {

        if (Geekplay.Instance.Platform != Platform.Editor)
        {
            try
            {
                //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, eventStr);
                AppMetrica.Instance.ReportEvent(eventStr);
            }
            catch (Exception e)
            {

            }
        }
        else
        {
            Debug.Log("Отправлен ивент:" + eventStr);
        }
    }
}

