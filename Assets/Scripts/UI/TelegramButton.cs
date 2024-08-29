using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegramButton : MonoBehaviour
{
    private readonly string tgUrl = "https://t.me/geekplay_ru";

    private void Start()
    {
        Geekplay.Instance.GameReady();
    }
    public void GoToTelegram()
    {
        Application.OpenURL(tgUrl);
    }
}
