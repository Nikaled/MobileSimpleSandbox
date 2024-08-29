using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationGameplay : MonoBehaviour
{
    public TextMeshProUGUI textYouGot;

    public TextMeshProUGUI text1;

    public TextMeshProUGUI Heli1;
    public TextMeshProUGUI Heli2;
    public TextMeshProUGUI Heli3;
    public TextMeshProUGUI Heli4;
    public TextMeshProUGUI Heli5;

    public TextMeshProUGUI Plane1;
    public TextMeshProUGUI Plane2;
    public TextMeshProUGUI Plane3;
    public TextMeshProUGUI Plane4;

    public TextMeshProUGUI ObjectInteraction;

    public TextMeshProUGUI BuildObject1;
    public TextMeshProUGUI BuildObject2;
    public TextMeshProUGUI BuildObject3;

    public TextMeshProUGUI DeletingObject1;
    public TextMeshProUGUI DeletingObject2;

    public TextMeshProUGUI RotatingObject1;
    public TextMeshProUGUI RotatingObject2;

    public TextMeshProUGUI IdleInstruction1;
    public TextMeshProUGUI IdleInstruction2;
    public TextMeshProUGUI IdleInstruction3;
    public TextMeshProUGUI IdleInstruction4;
    public TextMeshProUGUI IdleInstruction5;
    public TextMeshProUGUI IdleInstruction6;
    public TextMeshProUGUI IdleInstruction7;
    public TextMeshProUGUI IdleInstruction8;

    public TextMeshProUGUI CitizenEnterInstruction;

    public TextMeshProUGUI CarControl1;
    public TextMeshProUGUI CarControl2;
    public TextMeshProUGUI CarControl3;
    public TextMeshProUGUI CarControl4;
    public TextMeshProUGUI CarControl5;

    [Header("Mobile")]
    public TextMeshProUGUI LeftUpButton1;
    public TextMeshProUGUI LeftUpButton2;
    public TextMeshProUGUI LeftUpButton3;

    [Header("Multi")]
    public TextMeshProUGUI Weapon2;
    public TextMeshProUGUI Weapon3;
    public TextMeshProUGUI Weapon4;
    public TextMeshProUGUI Weapon5;

    public TextMeshProUGUI BuildMenu1;
    public TextMeshProUGUI BuildMenu2;
    public TextMeshProUGUI BuildMenu3;
    public TextMeshProUGUI BuildMenu4;
    public TextMeshProUGUI BuildMenu5;
    public TextMeshProUGUI BuildMenu6;
    public TextMeshProUGUI BuildMenu7;
    public TextMeshProUGUI BuildMenu8;
    public TextMeshProUGUI BuildMenu9;


    public TextMeshProUGUI RotatingObj1;
    public TextMeshProUGUI RotatingObj2;
    public TextMeshProUGUI RotatingObj3;
    public TextMeshProUGUI RotatingObj4;
    public TextMeshProUGUI RotatingObj5;
    public TextMeshProUGUI RotatingObj6;
    public TextMeshProUGUI RotatingObj7;
    public TextMeshProUGUI RotatingObj8;
    public TextMeshProUGUI RotatingObj9;
    public TextMeshProUGUI RotatingObj10;

    public TextMeshProUGUI SaveMapHeader;
    public TextMeshProUGUI MoreCoins;

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

    public TextMeshProUGUI AdWarningConfirm1;
    public TextMeshProUGUI AdWarningConfirm2;

    public TextMeshProUGUI GainMoneyBuildingButton;
    public TextMeshProUGUI ConfirmUIFromReward1;
    public TextMeshProUGUI ConfirmUIFromReward2;
    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            RuLocalization();
        }
        if (Geekplay.Instance.language == "en")
        {
            EnLocalization();
        }
        if (Geekplay.Instance.language == "tr")
        {
            TrLocalization();
        }
    }
    private void RuLocalization()
    {
        textYouGot.text = "Вы получите 1";

        text1.text = "<color=orange>[F]</color> Cесть";

        Heli1.text = "<color=orange>[W,A,S,D]</color> Наклон";
        Heli2.text = "<color=orange>[Q, E]</color> Поворот";
        Heli3.text = "<color=orange>[Пробел, Z]</color>Увеличить/уменьшить мощность";
        Heli4.text = "<color=orange>[X]</color> Включить/выключить двигатель ";
        Heli5.text = "<color=orange>[F]</color> Выйти";

        Plane1.text = "<color=orange>[удерживать Пробел]</color> Взлететь";
        Plane2.text = "<color=orange>[W,A,S,D]</color> Наклон";
        Plane3.text = "<color=orange>[Q, E]</color> Поворот";
        Plane4.text = "<color=orange>[F]</color> Выйти";

        ObjectInteraction.text = "<color=orange>[F]</color> Взаимодействовать";
        BuildObject1.text = "<color=orange>[ЛКМ]</color> Поставить объект";
        BuildObject2.text = "<color=orange>[R]</color> Повернуть";
        BuildObject3.text = "<color=orange>[B]</color> Выйти из режима";

        DeletingObject1.text = "<color=orange>[ЛКМ]</color> Удалить объект";
        DeletingObject2.text = "<color=orange>[N]</color> Выйти из режима";

        RotatingObject1.text = "<color=orange>[ЛКМ]</color> Выбрать объект";
        RotatingObject2.text = "<color=orange>[M]</color> Выйти из режима";

        IdleInstruction1.text = "<color=orange>[B]</color> Строительство";
        IdleInstruction2.text = "<color=orange>[N]</color> Удаление";
        IdleInstruction3.text = "<color=orange>[M]</color> Перестройка";
        IdleInstruction4.text = "<color=orange>[P]</color> Скриншот";
        IdleInstruction5.text = "<color=orange>[K]</color> Сохранение";
        IdleInstruction6.text = "<color=orange>[Q]</color> Сменить камеру";
        IdleInstruction7.text = "<color=orange>[I]</color> Магазин";
        IdleInstruction8.text = "<color=orange>[Tab]</color> В меню";

        CitizenEnterInstruction.text = "<color=orange>[F]</color> Поменяться телами";

        CarControl1.text = "<color=orange>[W,A,S,D]</color> Управление";
        CarControl2.text = "<color=orange>[Пробел]</color> Тормоз";
        CarControl3.text = "<color=orange>[Q]</color> Сменить камеру";
        CarControl4.text = "<color=orange>[F]</color> Выйти";
        CarControl5.text = "<color=orange>[ЛКМ]</color> Выстрел";

        LeftUpButton1.text = "Скриншот";
        LeftUpButton2.text = "Сохранить";
        LeftUpButton3.text = "В меню";

        Weapon2.text = "Пистолет";
        Weapon3.text = "Нож";
        Weapon4.text = "Рука";
        Weapon5.text = "Граната";

        BuildMenu1.text = "Машины";
        BuildMenu2.text = "Воздушный транспорт";
        BuildMenu3.text = "Аэропорт";
        BuildMenu4.text = "Ферма";
        BuildMenu5.text = "Военное";
        BuildMenu6.text = "Город";
        BuildMenu7.text = "Жители";
        BuildMenu8.text = "Животные";
        BuildMenu9.text = "Небо";

        RotatingObj1.text = "Размер X";
        RotatingObj2.text = "Размер Y";
        RotatingObj3.text = "Размер Z";
        RotatingObj4.text = "Поворот X";
        RotatingObj5.text = "Поворот Y";
        RotatingObj6.text = "Поворот Z";
        RotatingObj7.text = "РАЗМЕР";
        RotatingObj8.text = "ПОВОРОТ";
        RotatingObj9.text = "Применить";
        RotatingObj10.text = "Сбросить";

        SaveMapHeader.text = "ВЫБЕРИТЕ СЛОТ ДЛЯ СОХРАНЕНИЯ";
        MoreCoins.text = "Получить Больше";

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

        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;

        GainMoneyBuildingButton.text = "Получить 10 монет";
        ConfirmUIFromReward1.text = InAppShopConfirm1.text;
        ConfirmUIFromReward2.text = InAppShopConfirm2.text;
    }
    private void EnLocalization()
    {
        textYouGot.text = "you will get 1";

        text1.text = "<color=orange>[F]</color> Sit down";

        Heli1.text = "<color=orange>[W,A,S,D]</color> Pitch";
        Heli2.text = "<color=orange>[Q, E]</color> Roll";
        Heli3.text = "<color=orange>[Space, Z]</color>Increase/decrease power";
        Heli4.text = "<color=orange>[X]</color> Turn on/off the engine";
        Heli5.text = "<color=orange>[F]</color> Exit";

        Plane1.text = "<color=orange>[hold the Space bar]</color> Take off";
        Plane2.text = "<color=orange>[W,A,S,D]</color> Pitch";
        Plane3.text = "<color=orange>[Q, E]</color> Roll";
        Plane4.text = "<color=orange>[F]</color> Exit";

        ObjectInteraction.text = "<color=orange>[F]</color> Interact";
        BuildObject1.text = "<color=orange>[LMB]</color> Place object";
        BuildObject2.text = "<color=orange>[R]</color> Rotate";
        BuildObject3.text = "<color=orange>[B]</color> Exit mode";

        DeletingObject1.text = "<color=orange>[LMB]</color> Delete object";
        DeletingObject2.text = "<color=orange>[N]</color> Exit mode";

        RotatingObject1.text = "<color=orange>[LMB]</color> Select object";
        RotatingObject2.text = "<color=orange>[M]</color> Exit mode";

        IdleInstruction1.text = "<color=orange>[B]</color> Build";
        IdleInstruction2.text = "<color=orange>[N]</color> Delete";
        IdleInstruction3.text = "<color=orange>[M]</color> Rebuild";
        IdleInstruction4.text = "<color=orange>[P]</color> Screenshot";
        IdleInstruction5.text = "<color=orange>[K]</color> Save";
        IdleInstruction6.text = "<color=orange>[Q]</color> Change camera";
        IdleInstruction7.text = "<color=orange>[I]</color> Shop";
        IdleInstruction8.text = "<color=orange>[Tab]</color> To menu";

        CitizenEnterInstruction.text = "<color=orange>[F]</color> Swap bodies";

        CarControl1.text = "<color=orange>[W,A,S,D]</color> Movement";
        CarControl2.text = "<color=orange>[Space bar]</color> Brake";
        CarControl3.text = "<color=orange>[Q]</color> Change camera";
        CarControl4.text = "<color=orange>[F]</color> Exit";
        CarControl5.text = "<color=orange>[LMB]</color> Shoot";

        LeftUpButton1.text = "Screenshot";
        LeftUpButton2.text = "Save";
        LeftUpButton3.text = "Go to menu";

        Weapon2.text = "Pistol";
        Weapon3.text = "Knife";
        Weapon4.text = "Hand";
        Weapon5.text = "Grenade";

        BuildMenu1.text = "Cars";
        BuildMenu2.text = "Air transport";
        BuildMenu3.text = "Airport";
        BuildMenu4.text = "Farm";
        BuildMenu5.text = "Military";
        BuildMenu6.text = "City";
        BuildMenu7.text = "Citizens";
        BuildMenu8.text = "Animals";
        BuildMenu9.text = "Sky";

        RotatingObj1.text = "Size X";
        RotatingObj2.text = "Size Y";
        RotatingObj3.text = "Size Z";
        RotatingObj4.text = "Rotate X";
        RotatingObj5.text = "Rotate Y";
        RotatingObj6.text = "Rotate Z";
        RotatingObj7.text = "SIZE";
        RotatingObj8.text = "ROTATION";
        RotatingObj9.text = "Apply";
        RotatingObj10.text = "Reset";

        SaveMapHeader.text = "SELECT A SLOT TO SAVE";
        MoreCoins.text = "Get More";

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
        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;

        GainMoneyBuildingButton.text = "Get 10 coins";
        ConfirmUIFromReward1.text = InAppShopConfirm1.text;
        ConfirmUIFromReward2.text = InAppShopConfirm2.text;
    }
    private void TrLocalization()
    {
        textYouGot.text = "1 tane alacaksın";

        text1.text = "<color=orange>[F]</color> Otur";

        Heli1.text = "<color=orange>[W,A,S,D]</color> Eğim";
        Heli2.text = "<color=orange>[Q, E]</color> Döndürme";
        Heli3.text = "<color=orange>[Boşluk, Z]</color>Gücü artırın/azaltın";
        Heli4.text = "<color=orange>[X]</color>Motoru aç/kapat ";
        Heli5.text = "<color=orange>[F]</color> Çık";

        Plane1.text = "<color=orange>[Boşluğu tut]</color> Uçmak";
        Plane2.text = "<color=orange>[W,A,S,D]</color> Eğim";
        Plane3.text = "<color=orange>[Q, E]</color> Döndürme";
        Plane4.text = "<color=orange>[F]</color> Çık";

        ObjectInteraction.text = "<color=orange>[F]</color> Etkileşimde bulunun";
        BuildObject1.text = "<color=orange>[LMB]</color> Nesneyi koy";
        BuildObject2.text = "<color=orange>[R]</color> Döndür";
        BuildObject3.text = "<color=orange>[B]</color> Moddan çık";

        DeletingObject1.text = "<color=orange>[LMB]</color> Nesneyi sil";
        DeletingObject2.text = "<color=orange>[N]</color> Moddan çık";

        RotatingObject1.text = "<color=orange>[LMB]</color> Nesne seç";
        RotatingObject2.text = "<color=orange>[M]</color> Moddan çık";

        IdleInstruction1.text = "<color=orange>[B]</color> İnşaat";
        IdleInstruction2.text = "<color=orange>[N]</color> Silme";
        IdleInstruction3.text = "<color=orange>[M]</color> Yeniden yapılandırma";
        IdleInstruction4.text = "<color=orange>[P]</color> Ekran görüntüsü";
        IdleInstruction5.text = "<color=orange>[K]</color> Kaydetme";
        IdleInstruction6.text = "<color=orange>[Q]</color> Kamerayı değiştir";
        IdleInstruction7.text = "<color=orange>[I]</color> Mağaza";
        IdleInstruction8.text = "<color=orange>[Tab]</color> Menüye git";

        CitizenEnterInstruction.text = "<color=orange>[F]</color> Organları değiştir";

        CarControl1.text = "<color=orange>[W,A,S,D]</color> Yönetimi";
        CarControl2.text = "<color=orange>[Boşluk]</color> Fren";
        CarControl3.text = "<color=orange>[Q]</color> Kamerayı değiştir";
        CarControl4.text = "<color=orange>[F]</color> Çık";
        CarControl5.text = "<color=orange>[LMB]</color> Atış";


        LeftUpButton1.text = "Ekran görüntüsü";
        LeftUpButton2.text = "Kaydet";
        LeftUpButton3.text = "Menüde";

        Weapon2.text = "Tabanca";
        Weapon3.text = "Bıçak";
        Weapon4.text = "El";
        Weapon5.text = "El bombası";

        BuildMenu1.text = "Makineler";
        BuildMenu2.text = "Hava taşımacılığı";
        BuildMenu3.text = "Havaalanı";
        BuildMenu4.text = "Çiftlik";
        BuildMenu5.text = "Askeri";
        BuildMenu6.text = "Şehir";
        BuildMenu7.text = "Sakinler";
        BuildMenu8.text = "Hayvanlar";
        BuildMenu9.text = "Gökyüzü";

        RotatingObj1.text = "Boyut X";
        RotatingObj2.text = "Boyut Y";
        RotatingObj3.text = "Boyut Z";
        RotatingObj4.text = "dönüşü X";
        RotatingObj5.text = "dönüşü Y";
        RotatingObj6.text = "dönüşü Z";
        RotatingObj7.text = "BOYUT";
        RotatingObj8.text = "DÖNDÜRME";
        RotatingObj9.text = "Uygula";
        RotatingObj10.text = "Sıfırla";

        SaveMapHeader.text = "KAYDEDİLECEK YUVAYI SEÇİN";
        MoreCoins.text = "Daha Fazlasını alın";

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

        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;

        GainMoneyBuildingButton.text = "10 jeton alın";
        ConfirmUIFromReward1.text = InAppShopConfirm1.text;
        ConfirmUIFromReward2.text = InAppShopConfirm2.text;
    }
}
