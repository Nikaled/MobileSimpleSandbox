using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI otherGames;
    [SerializeField] TextMeshProUGUI onlyForAutho;
    [SerializeField] TextMeshProUGUI Button1;
    [SerializeField] TextMeshProUGUI Button2;
    [SerializeField] TextMeshProUGUI Button3;
    [SerializeField] TextMeshProUGUI Button4;
    [SerializeField] TextMeshProUGUI Button5;
    [SerializeField] TextMeshProUGUI LeadersHeadMain;
    [SerializeField] TextMeshProUGUI LeadersHead1;
    [SerializeField] TextMeshProUGUI LeadersHead2;
    [SerializeField] TextMeshProUGUI LeadersHead3;
    [SerializeField] TextMeshProUGUI LeadersDesc1;
    [SerializeField] TextMeshProUGUI LeadersDesc2;
    [SerializeField] TextMeshProUGUI LeadersDesc3;
    [SerializeField] TextMeshProUGUI ChooseMapH;
    [SerializeField] TextMeshProUGUI ChooseMap1;
    [SerializeField] TextMeshProUGUI ChooseMap2;
    [SerializeField] TextMeshProUGUI ChooseMap3;
    [SerializeField] TextMeshProUGUI ChooseMap4;
    [SerializeField] TextMeshProUGUI ChooseLoadMapH;
    [SerializeField] TextMeshProUGUI MenuButton1;
    [SerializeField] TextMeshProUGUI MenuButton2;
    [SerializeField] TextMeshProUGUI PromoAsk;
    [SerializeField] TextMeshProUGUI TelegramButton;
    [SerializeField] TextMeshProUGUI TakeButton;
    [SerializeField] TextMeshProUGUI PromoMessage1;
    [SerializeField] TextMeshProUGUI PromoMessage2;
    [SerializeField] TextMeshProUGUI PromoMessage3;
    [SerializeField] Text PlaceHolder;
    public TextMeshProUGUI ShopButton;
    public TextMeshProUGUI InAppShop1;
    public TextMeshProUGUI InAppShop2;
    public TextMeshProUGUI InAppShop3;
    public TextMeshProUGUI InAppShop4;
    public TextMeshProUGUI InAppShop5;
    public TextMeshProUGUI InAppShopB1;
    public TextMeshProUGUI InAppShopB2;
    public TextMeshProUGUI InAppShopB3;

    public TextMeshProUGUI InAppShopConfirm1;
    public TextMeshProUGUI InAppShopConfirm2;
    [SerializeField] TextMeshProUGUI TutorialButton;
    private void Start()
    {
      if(Geekplay.Instance.language == "ru")
        {
            RuLocalization();
        }
      else if(Geekplay.Instance.language == "en")
        {
            EnLocalization();
        }
        else if (Geekplay.Instance.language == "tr")
        {
            TrLocalization();
        }
    } 
    private void RuLocalization()
    {
        otherGames.text = "ДРУГИЕ ИГРЫ";
        onlyForAutho.text = "Награда только для авторизованных пользователей";
        Button1.text = "СОЗДАТЬ МИР";
        Button2.text = "ЗАГРУЗИТЬ МИР";
        Button3.text = "ЛИДЕРЫ";
        Button4.text = "БОНУСЫ";
        Button5.text = "НАШИ ИГРЫ";
        LeadersHeadMain.text = "ЛУЧШИЕ ИГРОКИ";
        LeadersHead1.text = "ЛУЧШИЕ СТРОИТЕЛИ";
        LeadersHead2.text = "ЛУЧШИЕ РАЗРУШИТЕЛИ";
        LeadersHead3.text = "ЛУЧШАЯ ПОДДЕРЖКА";
        LeadersDesc1.text = "Стройте как можно больше объектов!";
        LeadersDesc2.text = "Разрушайте все, что можете!";
        LeadersDesc3.text = "Поддержите разработчика донатом!";
        ChooseMapH.text = "ВЫБЕРИТЕ КАРТУ";
        ChooseMap1.text = "Поле";
        ChooseMap2.text = "Военная база";
        ChooseMap3.text = "Ферма";
        ChooseMap4.text = "Аэропорт";
        ChooseLoadMapH.text = "ВЫБЕРИТЕ КАРТУ";
        MenuButton1.text = "Меню";
        MenuButton2.text = "Меню";
        PromoAsk.text = "Подпишись на наш канал и введи промокод который найдешь там";
        PlaceHolder.text = "Введите код...";
        TelegramButton.text = "Наш канал";
        TakeButton.text = "Забрать";
        PromoMessage1.text = "Промокод успешно введен";
        PromoMessage2.text = "Промокод уже использован";
        PromoMessage3.text = "Такого промокода нет";

        InAppShop1.text = "МАГАЗИН";
        InAppShop2.text = "10 монет";
        InAppShop3.text = "50 монет";
        InAppShop4.text = "150 монет";
        InAppShop5.text = "300 монет";
        InAppShopB1.text = "20 Ян";
        InAppShopB2.text = "45 Ян";
        InAppShopB3.text = "60 Ян";

        InAppShopConfirm1.text = "Вы получили:";
        InAppShopConfirm2.text = "ОТЛИЧНО!";
        ShopButton.text = "Больше \n <color=orange>   Золота </color> ";
        TutorialButton.text = "ОБУЧЕНИЕ";
    }
    private void EnLocalization()
    {
        otherGames.text = "OTHER GAMES";
        onlyForAutho.text = "Reward only for authorized users";
        Button1.text = "CREATE A WORLD";
        Button2.text = "LOAD THE WORLD";
        Button3.text = "LEADERS";
        Button4.text = "BONUSES";
        Button5.text = "OUR GAMES";
        LeadersHeadMain.text = "TOP PLAYERS";
        LeadersHead1.text = "THE BEST BUILDERS";
        LeadersHead2.text = "THE BEST DESTROYERS";
        LeadersHead3.text = "BEST SUPPORT";
        LeadersDesc1.text = "Build as many objects as possible!";
        LeadersDesc2.text = "Destroy everything you can!";
        LeadersDesc3.text = "Support the developer with a donation!";
        ChooseMapH.text = "SELECT A MAP";
        ChooseMap1.text = "Field";
        ChooseMap2.text = "Military base";
        ChooseMap3.text = "Farm";
        ChooseMap4.text = "Airport";
        ChooseLoadMapH.text = "SELECT A MAP";
        MenuButton1.text = "Menu";
        MenuButton2.text = "Menu";
        PromoAsk.text = "Subscribe to our channel and enter the promo code that you will find there";
        PlaceHolder.text = "Enter the code...";
        TelegramButton.text = "Our Channel";
        TakeButton.text = "Enter";
        PromoMessage1.text = "Promo code successfully entered";
        PromoMessage2.text = "Promo code has already been used";
        PromoMessage3.text = "There is no such promo code";

        InAppShop1.text = "STORE";
        InAppShop2.text = "10 coins";
        InAppShop3.text = "50 coins";
        InAppShop4.text = "150 coins";
        InAppShop5.text = "300 coins";
        InAppShopB1.text = "20 Yan";
        InAppShopB2.text = "45 Yan";
        InAppShopB3.text = "60 Yan";

        InAppShopConfirm1.text = "You got:";
        InAppShopConfirm2.text = "GREAT!";

        ShopButton.text = "More \n <color=orange>   Gold </color> ";
        TutorialButton.text = "TUTORIAL";
    }
    private void TrLocalization()
    {
        otherGames.text = "DİĞER OYUNLAR";
        onlyForAutho.text = "Yalnızca yetkili kullanıcılar için";
        Button1.text = "DÜNYAYI YARATMAK";
        Button2.text = "DÜNYAYI YÜKLE";
        Button3.text = "LİDERLER";
        Button4.text = "BONUSLAR";
        Button5.text = "OYUNLARIMIZ";
        LeadersHeadMain.text = "EN İYİ OYUNCULAR";
        LeadersHead1.text = "EN İYİ İNŞAATÇILAR";
        LeadersHead2.text = "EN İYİ YIKICILAR";
        LeadersHead3.text = "EN İYİ DESTEK";
        LeadersDesc1.text = "Mümkün olduğunca çok nesne oluşturun!";
        LeadersDesc2.text = "Yapabileceğiniz her şeyi yok edin!";
        LeadersDesc3.text = "Geliştiriciyi bağışla destekleyin!";
        ChooseMapH.text = "KARTI SEÇİN";
        ChooseMap1.text = "Alan";
        ChooseMap2.text = "Askeri üs";
        ChooseMap3.text = "Çiftlik";
        ChooseMap4.text = "Havaalanı";
        ChooseLoadMapH.text = "KARTI SEÇİN";
        MenuButton1.text = "Menü";
        MenuButton2.text = "Menü";
        PromoAsk.text = "Kanalımıza abone olun ve orada bulduğunuz promosyon kodunu girin";
        PlaceHolder.text = "Kodu girin...";
        TelegramButton.text = "Kanalımız";
        TakeButton.text = "Al";
        PromoMessage1.text = "Promosyon Kodu başarıyla girildi";
        PromoMessage2.text = "Promosyon Kodu zaten kullanılıyor";
        PromoMessage3.text = "Böyle bir promosyon kodu yok";

        InAppShop1.text = "MAĞAZA";
        InAppShop2.text = "10 jeton";
        InAppShop3.text = "50 jeton";
        InAppShop4.text = "150 jeton";
        InAppShop5.text = "300 jeton";
        InAppShopB1.text = "20 Yan";
        InAppShopB2.text = "45 Yan";
        InAppShopB3.text = "60 Yan";

        InAppShopConfirm1.text = "Aldınız:";
        InAppShopConfirm2.text = "HARİKA!";
        ShopButton.text = "Altın \n <color=orange>   Alın </color> ";
        TutorialButton.text = "EĞİTİM";
    }
}
