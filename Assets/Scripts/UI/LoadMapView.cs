using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadMapView : MonoBehaviour
{
    [SerializeField] GameObject[] MapsEmptySlotImage;
    [SerializeField] TextMeshProUGUI[] MapsEmptySlotText;
    [SerializeField] TextMeshProUGUI mir1;
    [SerializeField] TextMeshProUGUI mir2;
    [SerializeField] TextMeshProUGUI mir3;
    [SerializeField] TextMeshProUGUI mir4;
    private string PustoiSlotLocalization;
    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            PustoiSlotLocalization = "Пустой слот";
            mir1.text = "Мир 1";
            mir2.text = "Мир 2";
            mir3.text = "Мир 3";
            mir4.text = "Мир 4";
        }
        if (Geekplay.Instance.language == "en")
        {
            PustoiSlotLocalization = "Empty Slot";
            mir1.text = "World 1";
            mir2.text = "World 2";
            mir3.text = "World 3";
            mir4.text = "World 4";
        }
        if (Geekplay.Instance.language == "tr")
        {
            PustoiSlotLocalization = "Boş yuva";
            mir1.text = "Alem 1";
            mir2.text = "Alem 2";
            mir3.text = "Alem 3";
            mir4.text = "Alem 4";
        }
        MapSlotsViewUpdate();
    }
    public void MapSlotsViewUpdate()
    {
        for (int i = 0; i < MapsEmptySlotText.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (Geekplay.Instance.PlayerData.NameMap1 != null && Geekplay.Instance.PlayerData.NameMap1 != string.Empty)
                    {
                        //MapsEmptySlotImage[i].SetActive(false);
                        MapsEmptySlotText[i].text = Geekplay.Instance.PlayerData.MapDate1;
                    }
                    else
                    {
                        //MapsEmptySlotImage[i].SetActive(true);
                        MapsEmptySlotText[i].text = PustoiSlotLocalization;
                    }

                    break;
                case 1:
                    if (Geekplay.Instance.PlayerData.NameMap2 != null && Geekplay.Instance.PlayerData.NameMap2 != string.Empty)
                    {
                        //MapsEmptySlotImage[i].SetActive(false);
                        MapsEmptySlotText[i].text = Geekplay.Instance.PlayerData.MapDate2;
                    }
                    else
                    {
                        //MapsEmptySlotImage[i].SetActive(true);
                        MapsEmptySlotText[i].text = PustoiSlotLocalization;
                    }

                    break;
                case 2:
                    if (Geekplay.Instance.PlayerData.NameMap3 != null && Geekplay.Instance.PlayerData.NameMap3 != string.Empty)
                    {
                        //MapsEmptySlotImage[i].SetActive(false);
                        MapsEmptySlotText[i].text = Geekplay.Instance.PlayerData.MapDate3;

                    }
                    else
                    {
                        //MapsEmptySlotImage[i].SetActive(true);
                        MapsEmptySlotText[i].text = PustoiSlotLocalization;
                    }

                    break;
                case 3:
                    if (Geekplay.Instance.PlayerData.NameMap4 != null && Geekplay.Instance.PlayerData.NameMap4 != string.Empty)
                    {
                        //MapsEmptySlotImage[i].SetActive(false);
                        MapsEmptySlotText[i].text = Geekplay.Instance.PlayerData.MapDate4;
                    }
                    else
                    {
                        //MapsEmptySlotImage[i].SetActive(true);
                        MapsEmptySlotText[i].text = PustoiSlotLocalization;
                    }
                    break;
            }
        }
    }
}
