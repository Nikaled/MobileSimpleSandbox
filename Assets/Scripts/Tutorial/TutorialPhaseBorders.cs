using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhaseBorders : MonoBehaviour
{
    public PhaseBorder ThisBorderPhase;
    public enum PhaseBorder
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendAnalytics();
        }
        if (other.gameObject.layer == 11) // 11 - Enter collider for transport 
        {
            var EnterScript = other.GetComponent<EnterController>();
            if (EnterScript != null)
            {
                if (EnterScript.IsPlayerIn == true)
                {
                    SendAnalytics();
                }
            }
        }
    }
    private void SendAnalytics()
    {
        if(Geekplay.Instance.PlayerData.PhaseBordersCompleteState == null)
        {
            Geekplay.Instance.PlayerData.PhaseBordersCompleteState = new bool[5];
        }
        if (Geekplay.Instance.PlayerData.PhaseBordersCompleteState.Length < 5)
        {
            Geekplay.Instance.PlayerData.PhaseBordersCompleteState = new bool[5];
        }
        switch (ThisBorderPhase)
        {
            case PhaseBorder.Phase1:
                if(Geekplay.Instance.PlayerData.PhaseBordersCompleteState[0] == false)
                {
                    Geekplay.Instance.PlayerData.PhaseBordersCompleteState[0] = true;
                    Analytics.instance.SendEvent("Tutorial_1_PhaseBorderCompleted_Building");
                }
                break;
            case PhaseBorder.Phase2:
                if (Geekplay.Instance.PlayerData.PhaseBordersCompleteState[1] == false)
                {
                    Geekplay.Instance.PlayerData.PhaseBordersCompleteState[1] = true;
                    Analytics.instance.SendEvent("Tutorial_2_PhaseBorderCompleted_Shooting");
                }
                break;
            case PhaseBorder.Phase3:
                if (Geekplay.Instance.PlayerData.PhaseBordersCompleteState[2] == false)
                {
                    Geekplay.Instance.PlayerData.PhaseBordersCompleteState[2] = true;
                    Analytics.instance.SendEvent("Tutorial_3_PhaseBorderCompleted_AnimalRiding");
                }
                break;
            case PhaseBorder.Phase4:
                if (Geekplay.Instance.PlayerData.PhaseBordersCompleteState[3] == false)
                {
                    Geekplay.Instance.PlayerData.PhaseBordersCompleteState[3] = true;
                    Analytics.instance.SendEvent("Tutorial_4_PhaseBorderCompleted_Cars");
                }
                break;
            case PhaseBorder.Phase5:
                if (Geekplay.Instance.PlayerData.PhaseBordersCompleteState[4] == false)
                {
                    Geekplay.Instance.PlayerData.PhaseBordersCompleteState[4] = true;
                    Analytics.instance.SendEvent("Tutorial_5_PhaseBorderCompleted_EndTutorial");
                }
                break;
               
        }
        Geekplay.Instance.Save();
    }
}
