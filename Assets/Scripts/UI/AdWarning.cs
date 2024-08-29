using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdWarning : MonoBehaviour
{
    public int TimeToShowWarning;
    public GameObject WarningPanel;
    public TextMeshProUGUI WarningText;
    [SerializeField] GameObject AddCoinsConfirmUI;
    private void Start()
    {
        StartCoroutine(AwaitAndShowWarningPanel());
        LocalizateText(5);
        Geekplay.Instance.ShowedAdInEditor += ResumeTime;
    }
    private void OnEnable()
    {
        if(Geekplay.Instance !=null)
        Geekplay.Instance.ShowedAdInEditor += ResumeTime;
    }
    private void OnDisable()
    {
        Geekplay.Instance.ShowedAdInEditor -= ResumeTime;
    }
    private IEnumerator AwaitAndShowWarningPanel()
    {
        yield return new WaitForSeconds(TimeToShowWarning);
        WarningPanel.SetActive(true);
        StartCoroutine(StartTimer());
    }
    private void ResumeTime()
    {
        Time.timeScale = 1;
    }
    private IEnumerator StartTimer()
    {
        Player.instance.AdWarningActive = true;
        Cursor.lockState = CursorLockMode.None;
        Geekplay.Instance.IsAdWarningShowing = true;
        Time.timeScale = 0f;
        int Timer = 5;
        while (Timer != 0)
        {
            LocalizateText(Timer);
            Timer--;
            yield return new WaitForSecondsRealtime(1f);
        }
        Geekplay.Instance.ShowInterstitialAd();
        Geekplay.Instance.IsAdWarningShowing = false;
        Geekplay.Instance.PlayerData.Coins++;
        Geekplay.Instance.Save();
        WarningPanel.SetActive(false);
        AddCoinsConfirmUI.SetActive(true);
        Time.timeScale = 1f;
        Player.instance.AdWarningActive = false;
        StartCoroutine(AwaitAndShowWarningPanel());
        Cursor.lockState = CursorLockMode.None;
//#if UNITY_EDITOR
//        CanvasManager.instance.CheckActiveUnlockCursorWindows();
//#endif
    }

    private void LocalizateText(int Timer)
    {
        if (Geekplay.Instance.language == "ru")
            WarningText.text = $"Реклама через: {Timer}";
        if (Geekplay.Instance.language == "en")
            WarningText.text = $"Advertisement in: {Timer}";
        if (Geekplay.Instance.language == "tr")
            WarningText.text = $"Saniye sonra reklam verin: {Timer}";

    }
}