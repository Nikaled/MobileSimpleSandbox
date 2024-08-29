using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverBuildingMenu : MonoBehaviour
{
    [SerializeField] BuildingContentManager[] ContentManagers;
    List<List<bool>> BuildingMenuDataList = new();
    public static SaverBuildingMenu instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Geekplay.Instance.PlayerData.IsNotFirstPlay = true;
        //Geekplay.Instance.PlayerData.IsFirstPlay = true;


        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.CarsOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.AirTransportOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.AirportOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.FarmOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.MilitaryOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.CityOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.CitizensOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.AnimalsOpened);
        BuildingMenuDataList.Add(Geekplay.Instance.PlayerData.SkyOpened);

        //Geekplay.Instance.Save();
        if (Geekplay.Instance.PlayerData.IsNotFirstPlay == true)
        {
            LoadItemsState();
        }
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            Geekplay.Instance.PlayerData.Coins += 15;
            Geekplay.Instance.Save();
        }
#endif
    }
    public void ClearSave()
    {
        for (int i = 0; i < BuildingMenuDataList.Count; i++)
        {
            for (int j = 0; j < BuildingMenuDataList[i].Count; j++)
            {
                BuildingMenuDataList[i][j] = false;
            }
        }
        Geekplay.Instance.PlayerData.IsNotFirstPlay = false;
        Geekplay.Instance.Save();
    }
    public void SaveItemsState(List<bool> contentState, BuildingContentManager ContentManager)
    {
        Geekplay.Instance.PlayerData.IsNotFirstPlay = true;
       int ContentManagerIndex = -1;
        for (int i = 0; i < ContentManagers.Length; i++)
        {
            if(ContentManager == ContentManagers[i])
            {
                ContentManagerIndex = i;
            }
        }

        BuildingMenuDataList[ContentManagerIndex].Clear();
        BuildingMenuDataList[ContentManagerIndex].AddRange(contentState);
        Geekplay.Instance.Save();
    }
    public void LoadItemsState()
    {
        for (int i = 0; i < BuildingMenuDataList.Count; i++)
        {

            List<bool> States = BuildingMenuDataList[i];
            if(States == null)
            {
                return;
            }
            for (int j = 0; j < ContentManagers[i].CellsInGrid.Length; j++)
            {
                if(j > States.Count-1)
                {
                    continue;
                }
                ContentManagers[i].CellsInGrid[j].IsOpened = States[j];
                ContentManagers[i].CellsInGrid[j].CheckPriceVisible();
            }
        }
    }
}
