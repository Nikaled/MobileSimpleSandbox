using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SerializeBuildingManager : MonoBehaviour
{
    [SerializeField] GameObject[] AllPrefabsInGame;
    public static SerializeBuildingManager instance;
     public List<SerializedBuilding> BuildingsOnScene = new();
    [HideInInspector] public List<SerializedBuildingData> BuildingData = new();
    public GameObject SceneObjectsParent;
    public BuildingContentManager SkyContentManager;
    [HideInInspector] public int SkyIndex;
    [SerializeField] private TextMeshProUGUI buildingsCountText;

    private void Awake()
    {
        instance = this;
    }
    private void WriteDate(ref string MapDate)
    {
        DateTime date1 = DateTime.Now;
        MapDate = date1.ToString("HH:mm  dd.MM.yyyy");
    }
    private void LoadSky(int Index)
    {
        SkyBuildingCell SkyCell = SkyContentManager.CellsInGrid[Index].GetComponent<SkyBuildingCell>();
        SkyCell.ChangeSky();
    }
    private void SaveSky(ref int SkyInPlayerData)
    {
        SkyInPlayerData = SkyIndex;
    }
    public void ToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ReloadSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void TryLoadPlayerData(int SkinIndex, int TextureIndex, Vector3 position)
    {
        SerializePlayer.instance.LoadSkinByIndex(SkinIndex);
        SerializePlayer.instance.LoadTextureByIndex(TextureIndex);
        SerializePlayer.instance.LoadPlayerPosition(position);
    }
    private void SavePlayerData(ref int SkinSlot, ref int TextureSlot, ref Vector3 Position)
    {
        SkinSlot = SerializePlayer.instance.SaveSkinIndex();
        TextureSlot = SerializePlayer.instance.SaveTextureIndex();
        Position = SerializePlayer.instance.SavePlayerPosition();
    }
    public void SaveInMapSlot(int number)
    {
        if (BuildingsOnScene.Count > 50)
            return;
        BuildingData = new();

        for (int i = 0; i < BuildingsOnScene.Count; i++)
        {
            BuildingData.Add(BuildingsOnScene[i].SaveBuilding());
        }
        Geekplay.Instance.PlayerData.BuildingData = new();
        Geekplay.Instance.PlayerData.BuildingData.AddRange(BuildingData);
        Debug.Log("Ñîõðàíåíî îáúåêòîâ: " + Geekplay.Instance.PlayerData.BuildingData.Count);
        switch (number)
        {
            case 1:
                Geekplay.Instance.PlayerData.BuildingDataMap1 = new();
                Geekplay.Instance.PlayerData.BuildingDataMap1 = Geekplay.Instance.PlayerData.BuildingData;
                Geekplay.Instance.PlayerData.NameMap1 = SceneManager.GetActiveScene().name;
                SavePlayerData(ref Geekplay.Instance.PlayerData.PlayerSkin1, ref Geekplay.Instance.PlayerData.PlayerTexture1, ref Geekplay.Instance.PlayerData.PlayerPositionMap1);
                WriteDate(ref Geekplay.Instance.PlayerData.MapDate1);
                SaveSky(ref Geekplay.Instance.PlayerData.CurrentSkyIndexMap1);
                break;
            case 2:
                Geekplay.Instance.PlayerData.BuildingDataMap2 = new();
                Geekplay.Instance.PlayerData.BuildingDataMap2 = Geekplay.Instance.PlayerData.BuildingData;
                Geekplay.Instance.PlayerData.NameMap2 = SceneManager.GetActiveScene().name;
                SavePlayerData(ref Geekplay.Instance.PlayerData.PlayerSkin2, ref Geekplay.Instance.PlayerData.PlayerTexture2, ref Geekplay.Instance.PlayerData.PlayerPositionMap2);
                WriteDate(ref Geekplay.Instance.PlayerData.MapDate2);
                SaveSky(ref Geekplay.Instance.PlayerData.CurrentSkyIndexMap2);
                break;
            case 3:
                Geekplay.Instance.PlayerData.BuildingDataMap3 = new();
                Geekplay.Instance.PlayerData.BuildingDataMap3 = Geekplay.Instance.PlayerData.BuildingData;
                Geekplay.Instance.PlayerData.NameMap3 = SceneManager.GetActiveScene().name;
                SavePlayerData(ref Geekplay.Instance.PlayerData.PlayerSkin3, ref Geekplay.Instance.PlayerData.PlayerTexture3, ref Geekplay.Instance.PlayerData.PlayerPositionMap3);
                WriteDate(ref Geekplay.Instance.PlayerData.MapDate3);
                SaveSky(ref Geekplay.Instance.PlayerData.CurrentSkyIndexMap3);
                break;
            case 4:
                Geekplay.Instance.PlayerData.BuildingDataMap4 = new();
                Geekplay.Instance.PlayerData.BuildingDataMap4 = Geekplay.Instance.PlayerData.BuildingData;
                Geekplay.Instance.PlayerData.NameMap4 = SceneManager.GetActiveScene().name;
                SavePlayerData(ref Geekplay.Instance.PlayerData.PlayerSkin4, ref Geekplay.Instance.PlayerData.PlayerTexture4, ref Geekplay.Instance.PlayerData.PlayerPositionMap4);
                WriteDate(ref Geekplay.Instance.PlayerData.MapDate4);
                SaveSky(ref Geekplay.Instance.PlayerData.CurrentSkyIndexMap4);
                break;
        }
        BuildingData.Clear();
        Geekplay.Instance.Save();
        //Geekplay.Instance.PlayerData.BuildingData.Clear();
    }
    public int FindObjectPrefabIndex(string buildingName)
    {
        for (int i = 0; i < AllPrefabsInGame.Length; i++)
        {
            if (buildingName == AllPrefabsInGame[i]?.GetComponent<SerializedBuilding>()?.ObjectName)
            {
                return i;
            }
        }
        Debug.Log("Íå íàéäåí ïðåôàá â ñïèñêå:" + buildingName);
        return -1;
    }
    private void Start()
    {
        if (Geekplay.Instance.PlayerData.IsPlayerMapLoad)
        {
            LoadBuildingsFromSlot();
            Geekplay.Instance.PlayerData.IsPlayerMapLoad = false;
            Destroy(SceneObjectsParent);
            Geekplay.Instance.Save();
            Input.GetKeyDown(KeyCode.K);
        }
    }
    private void LoadBuildingsFromSlot()
    {
        switch (Geekplay.Instance.PlayerData.CurrentSaveSlotLoading)
        {
            case 1:
                LoadBuildings(Geekplay.Instance.PlayerData.BuildingDataMap1);
                TryLoadPlayerData(Geekplay.Instance.PlayerData.PlayerSkin1, Geekplay.Instance.PlayerData.PlayerTexture1, Geekplay.Instance.PlayerData.PlayerPositionMap1);
                LoadSky(Geekplay.Instance.PlayerData.CurrentSkyIndexMap1);
                break;
            case 2:
                LoadBuildings(Geekplay.Instance.PlayerData.BuildingDataMap2);
                TryLoadPlayerData(Geekplay.Instance.PlayerData.PlayerSkin2, Geekplay.Instance.PlayerData.PlayerTexture2, Geekplay.Instance.PlayerData.PlayerPositionMap2);
                LoadSky(Geekplay.Instance.PlayerData.CurrentSkyIndexMap2);
                break;
            case 3:
                LoadBuildings(Geekplay.Instance.PlayerData.BuildingDataMap3);
                TryLoadPlayerData(Geekplay.Instance.PlayerData.PlayerSkin3, Geekplay.Instance.PlayerData.PlayerTexture3, Geekplay.Instance.PlayerData.PlayerPositionMap3);
                LoadSky(Geekplay.Instance.PlayerData.CurrentSkyIndexMap3);
                break;
            case 4:
                LoadBuildings(Geekplay.Instance.PlayerData.BuildingDataMap4);
                TryLoadPlayerData(Geekplay.Instance.PlayerData.PlayerSkin4, Geekplay.Instance.PlayerData.PlayerTexture4, Geekplay.Instance.PlayerData.PlayerPositionMap4);
                LoadSky(Geekplay.Instance.PlayerData.CurrentSkyIndexMap4);
                break;
        }

    }
    private void Update()
    {
        if (Player.instance.InterfaceActive)
        {
            return;
        }
        if (Geekplay.Instance.language == "ru")
        {
        	if (BuildingsOnScene.Count <= 50)
            	buildingsCountText.text = $"Сохранить можно карту с максимум 50 объектами. У вас: {BuildingsOnScene.Count}/50";
        	else
            	buildingsCountText.text = $"<color=red>Сохранить можно карту с максимум 50 объектами. У вас: {BuildingsOnScene.Count}/50</color>";
        }
        else if (Geekplay.Instance.language == "en")
        {
        	if (BuildingsOnScene.Count <= 50)
            	buildingsCountText.text = $"You can save a map with a maximum of 50 objects. You have: {BuildingsOnScene.Count}/50";
        	else
            	buildingsCountText.text = $"<color=red>You can save a map with a maximum of 50 objects. You have: {BuildingsOnScene.Count}/50</color>";
        }
        else if (Geekplay.Instance.language == "tr")
        {
        	if (BuildingsOnScene.Count <= 50)
            	buildingsCountText.text = $"En fazla 50 nesne içeren bir haritayı kaydedebilirsiniz. Elinizde: {BuildingsOnScene.Count}/50";
        	else
            	buildingsCountText.text = $"<color=red>En fazla 50 nesne içeren bir haritayı kaydedebilirsiniz. Elinizde: {BuildingsOnScene.Count}/50</color>";
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Geekplay.Instance?.PlayerData?.BuildingData?.Count > 0)
            {
                LoadBuildings(Geekplay.Instance.PlayerData.BuildingDataMap1);
                TryLoadPlayerData(Geekplay.Instance.PlayerData.PlayerSkin1, Geekplay.Instance.PlayerData.PlayerTexture1, Geekplay.Instance.PlayerData.PlayerPositionMap1);
            }
        }
#endif
    }
    public void SaveBuildings()
    {
        if (BuildingsOnScene.Count <= 50)
        {
            StartCoroutine(SAVE());
        }
    }

    IEnumerator SAVE()
    {   
        for (int i = 0; i < BuildingsOnScene.Count; i++)
        {
            BuildingData.Add(BuildingsOnScene[i].SaveBuilding());
            yield return new WaitForSeconds(0.01f);
        }
        Geekplay.Instance.PlayerData.BuildingData = new();
        Geekplay.Instance.PlayerData.BuildingData.AddRange(BuildingData);
        Debug.Log("Ñîõðàíåíî îáúåêòîâ: " + Geekplay.Instance.PlayerData.BuildingData.Count);
        Geekplay.Instance.Save();
    }

    private void LoadBuildings(List<SerializedBuildingData> BuildingData)
    {
        StartCoroutine(L(BuildingData));
        Debug.Log("Çàãðóæåíî îáúåêòîâ:" + BuildingData.Count);
    }

    IEnumerator L(List<SerializedBuildingData> BuildingData)
    {
        for (int i = 0; i < BuildingData.Count; i++)
        {
            if (BuildingData.Count == 0) break;
            GameObject LoadedObj = Instantiate(AllPrefabsInGame[BuildingData[i].BuildingIndex]);
            SerializedBuilding SerObj = LoadedObj.GetComponent<SerializedBuilding>();
            if (SerObj != null)
            {
                SerObj.LoadBuilding(BuildingData[i]);
            }

            yield return new WaitForSeconds(0.1f);
        }
        SaveBuildings();
    }
}
