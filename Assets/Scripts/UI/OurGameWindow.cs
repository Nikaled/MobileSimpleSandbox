using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OurGameWindow : MonoBehaviour
{
    [SerializeField] private Button geometryDashButton;
    [SerializeField] private Button cloesChangeRewardButton;
    [SerializeField] private Button slapBattleRewardButton;
    [SerializeField] private Button twoPlayerGameRewardButton;

    [SerializeField] private TextMeshProUGUI geometryDashButtonTextView;
    [SerializeField] private TextMeshProUGUI cloesChangeRewardButtonTextView;
    [SerializeField] private TextMeshProUGUI slapBattleRewardButtonTextView;
    [SerializeField] private TextMeshProUGUI twoPlayerGameRewardButtonTextView;

    [SerializeField] private TextMeshProUGUI cloesChangeName;
    [SerializeField] private TextMeshProUGUI slapBattleName;
    [SerializeField] private TextMeshProUGUI twoPlayerGameName;
    public int CoinsForPlaying = 50;
    void Start()
    {
        Geekplay.Instance.OurGame = this;

        DisableGameToggle(284619);
        DisableGameToggle(295391);
        DisableGameToggle(289951);
        DisableGameToggle(227711);

        if (Geekplay.Instance.Platform == Platform.Yandex)
        {
            //Utils.CheckPlayGame(284619);
            //Utils.CheckPlayGame(295391);
            //Utils.CheckPlayGame(289951);
            //Utils.CheckPlayGame(227711);
        }
        LocalizateNames();
    }
    private void LocalizateNames()
    {
        if(Geekplay.Instance.language == "ru")
        {
            cloesChangeName.text = "Создай Королеву - Салон Уэнсдей";
            slapBattleName.text = "Битва Шлепков | Робби Обби";
            twoPlayerGameName.text = "Игры На Двоих: Дуэль";
        }
        else if(Geekplay.Instance.language == "en")
        {
            cloesChangeName.text = "Create a Queen - Wednesday Salon";
            slapBattleName.text = "Slap Battles | Robbi Obbi";
            twoPlayerGameName.text = "Games For Two: Duel";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            cloesChangeName.text = "Kralicesini Yaratin - Carsamba Salonu";
            slapBattleName.text = "Savas Tokadi | Robbi Obbi";
            twoPlayerGameName.text = "Iki Kisilik Oyunlar: Duello";
        }
    }
    public void EnabledGameToggle(int id)
    {
        if (id == 284619)
        {
            if (!Geekplay.Instance.PlayerData.IsGeometryDashRewardTaked)
            {
                geometryDashButton.onClick.RemoveAllListeners();
                geometryDashButton.interactable = true;
                geometryDashButton.onClick.AddListener(() =>
                {
                    TakeReward(284619, geometryDashButton, geometryDashButtonTextView);
                    Geekplay.Instance.PlayerData.IsGeometryDashRewardTaked = true;
                    Geekplay.Instance.Save();
                });

                if (Geekplay.Instance.language == "ru")
                {
                    geometryDashButtonTextView.text = "Забрать";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    geometryDashButtonTextView.text = "Claim";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    geometryDashButtonTextView.text = "Iddia";
                }
            }
            else
            {
                geometryDashButton.interactable = false;

                if (Geekplay.Instance.language == "ru")
                {
                    geometryDashButtonTextView.text = "Забрали";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    geometryDashButtonTextView.text = "Claimed";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    geometryDashButtonTextView.text = "talep edildi";
                }
            }
        }
        else if (id == 295391)
        {
            if (!Geekplay.Instance.PlayerData.IsCloesChangeRewardTaked)
            {
                cloesChangeRewardButton.onClick.RemoveAllListeners();
                cloesChangeRewardButton.interactable = true;
                cloesChangeRewardButton.onClick.AddListener(() =>
                {
                    TakeReward(295391, cloesChangeRewardButton, cloesChangeRewardButtonTextView);
                    Geekplay.Instance.PlayerData.IsCloesChangeRewardTaked = true;
                    Geekplay.Instance.Save();
                });

                if (Geekplay.Instance.language == "ru")
                {
                    cloesChangeRewardButtonTextView.text = "Забрать";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    cloesChangeRewardButtonTextView.text = "Claim";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    cloesChangeRewardButtonTextView.text = "Iddia";
                }
            }
            else
            {
                cloesChangeRewardButton.interactable = false;

                if (Geekplay.Instance.language == "ru")
                {
                    cloesChangeRewardButtonTextView.text = "Забрали";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    cloesChangeRewardButtonTextView.text = "Claimed";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    cloesChangeRewardButtonTextView.text = "talep edildi";
                }
            }
        }
        else if (id == 289951)
        {
            if (!Geekplay.Instance.PlayerData.IsSlapBattleRewardTaked)
            {
                slapBattleRewardButton.onClick.RemoveAllListeners();
                slapBattleRewardButton.interactable = true;
                slapBattleRewardButton.onClick.AddListener(() =>
                {
                    TakeReward(289951, slapBattleRewardButton, slapBattleRewardButtonTextView);
                    Geekplay.Instance.PlayerData.IsSlapBattleRewardTaked = true;
                    Geekplay.Instance.Save();
                });

                if (Geekplay.Instance.language == "ru")
                {
                    slapBattleRewardButtonTextView.text = "Забрать";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    slapBattleRewardButtonTextView.text = "Claim";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    slapBattleRewardButtonTextView.text = "Iddia";
                }
            }
            else
            {
                slapBattleRewardButton.interactable = false;

                if (Geekplay.Instance.language == "ru")
                {
                    slapBattleRewardButtonTextView.text = "Забрали";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    slapBattleRewardButtonTextView.text = "Claimed";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    slapBattleRewardButtonTextView.text = "talep edildi";
                }
            }
        }
        else if (id == 227711)
        {
            if (!Geekplay.Instance.PlayerData.IsTwoPlayerGameRewardTaked)
            {
                twoPlayerGameRewardButton.onClick.RemoveAllListeners();
                twoPlayerGameRewardButton.interactable = true;
                twoPlayerGameRewardButton.onClick.AddListener(() =>
                {
                    TakeReward(227711, twoPlayerGameRewardButton, twoPlayerGameRewardButtonTextView);
                    Geekplay.Instance.PlayerData.IsTwoPlayerGameRewardTaked = true;
                    Geekplay.Instance.Save();
                });

                if (Geekplay.Instance.language == "ru")
                {
                    twoPlayerGameRewardButtonTextView.text = "Забрать";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    twoPlayerGameRewardButtonTextView.text = "Claim";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    twoPlayerGameRewardButtonTextView.text = "Iddia";
                }
            }
            else
            {
                twoPlayerGameRewardButton.interactable = false;

                if (Geekplay.Instance.language == "ru")
                {
                    twoPlayerGameRewardButtonTextView.text = "Забрали";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    twoPlayerGameRewardButtonTextView.text = "Claimed";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    twoPlayerGameRewardButtonTextView.text = "talep edildi";
                }
            }
        }

        Geekplay.Instance.Save();
    }

    public void DisableGameToggle(int id)
    {
        if (id == 284619)
        {
            geometryDashButton.onClick.RemoveAllListeners();

            geometryDashButton.interactable = true;

            geometryDashButton.onClick.AddListener(() =>
            {
                OpenGame(284619);
            });

            if (Geekplay.Instance.language == "ru")
            {
                geometryDashButtonTextView.text = "Играть";
            }
            else if (Geekplay.Instance.language == "en")
            {
                geometryDashButtonTextView.text = "Play";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                geometryDashButtonTextView.text = "Oynamak";
            }
        }
        else if (id == 295391)
        {
            cloesChangeRewardButton.onClick.RemoveAllListeners();

            cloesChangeRewardButton.interactable = true;

            cloesChangeRewardButton.onClick.AddListener(() =>
            {
                OpenGame(295391);
            });

            if (Geekplay.Instance.language == "ru")
            {
                cloesChangeRewardButtonTextView.text = "Играть";
            }
            else if (Geekplay.Instance.language == "en")
            {
                cloesChangeRewardButtonTextView.text = "Play";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                cloesChangeRewardButtonTextView.text = "Oynamak";
            }
        }
        else if (id == 289951)
        {
            slapBattleRewardButton.onClick.RemoveAllListeners();

            slapBattleRewardButton.interactable = true;

            slapBattleRewardButton.onClick.AddListener(() =>
            {
                OpenGame(289951);
            });

            if (Geekplay.Instance.language == "ru")
            {
                slapBattleRewardButtonTextView.text = "Играть";
            }
            else if (Geekplay.Instance.language == "en")
            {
                slapBattleRewardButtonTextView.text = "Play";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                slapBattleRewardButtonTextView.text = "Oynamak";
            }
        }
        else if (id == 227711)
        {
            twoPlayerGameRewardButton.onClick.RemoveAllListeners();

            twoPlayerGameRewardButton.interactable = true;

            twoPlayerGameRewardButton.onClick.AddListener(() =>
            {
                OpenGame(227711);
            });

            if (Geekplay.Instance.language == "ru")
            {
                twoPlayerGameRewardButtonTextView.text = "Играть";
            }
            else if (Geekplay.Instance.language == "en")
            {
                twoPlayerGameRewardButtonTextView.text = "Play";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                twoPlayerGameRewardButtonTextView.text = "Oynamak";
            }
        }

        Geekplay.Instance.Save();
    }

    public void CheckIsPlayerPlayedGame(int IdGame)
    {
        //if (Geekplay.Instance.Platform == Platform.Yandex)
            //Utils.CheckPlayGame(IdGame);
    }

    public void TakeReward(int id, Button button, TextMeshProUGUI text)
    {
        //Wallet.AddMoneyST(100);
        Geekplay.Instance.PlayerData.Coins += CoinsForPlaying;
        DisableTakePossibility(button, text);
    }

    public void DisableTakePossibility(Button button, TextMeshProUGUI text)
    {
        button.interactable = false;

        if (Geekplay.Instance.language == "ru")
        {
            text.text = "Получено";
        }
        else if (Geekplay.Instance.language == "en")
        {
            text.text = "Taked";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            text.text = "Alindi";
        }
    }

    public void OpenGame(int appID)
    {
        switch (Geekplay.Instance.Platform)
        {
            case Platform.Editor:

                Debug.Log($"<color={Color.yellow}>OPEN OTHER GAMES</color>");

                break;
            case Platform.Yandex:
                //var domain = //Utils.GetDomain();
                //Application.OpenURL($"https://yandex.{domain}/games/#app={appID}");
                break;
        }
    }

    public void OpenOtherGame()
    {
        Geekplay.Instance.OpenOtherGames();
    }
}
