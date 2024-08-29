using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int Coins { 
        get
        {
            return _coinsDontUse;
        }
        set
        {
            _coinsDontUse = value;
            CoinsChanged?.Invoke(_coinsDontUse);
        }
    }

    public int myDonat;

    public bool IsGeometryDashRewardTaked;
    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;

    public event Action<int> CoinsChanged;
    public int _coinsDontUse;

    public bool IsNotFirstPlay;

    public int BuildCount;
    public int DestroyCount;

    public List<int> Codes;
    public List<bool> CarsOpened;   
    public List<bool> AirTransportOpened;   
    public List<bool> AirportOpened;   
    public List<bool> FarmOpened;
    public List<bool> MilitaryOpened;   
    public List<bool> CityOpened;   
    public List<bool> CitizensOpened;   
    public List<bool> AnimalsOpened;
    public List<bool> SkyOpened;
    public List<SerializedBuildingData> BuildingData;
    public List<SerializedBuildingData> BuildingDataMap1;
    public List<SerializedBuildingData> BuildingDataMap2;
    public List<SerializedBuildingData> BuildingDataMap3;
    public List<SerializedBuildingData> BuildingDataMap4;
    public string NameMap1;
    public string NameMap2;
    public string NameMap3;
    public string NameMap4;

    public string MapDate1;
    public string MapDate2;
    public string MapDate3;
    public string MapDate4;

    public int PlayerSkin1;
    public int PlayerSkin2;
    public int PlayerSkin3;
    public int PlayerSkin4;
    public int PlayerTexture1;
    public int PlayerTexture2;
    public int PlayerTexture3;
    public int PlayerTexture4;

    public Vector3 PlayerPositionMap1;
    public Vector3 PlayerPositionMap2;
    public Vector3 PlayerPositionMap3;
    public Vector3 PlayerPositionMap4;
    public int CurrentSaveSlotLoading;
    public bool IsPlayerMapLoad;
    public int CurrentSkyIndexMap1;
    public int CurrentSkyIndexMap2;
    public int CurrentSkyIndexMap3;
    public int CurrentSkyIndexMap4;
    public bool[] PhaseBordersCompleteState;
    /////InApps//////
    public string lastBuy;

    public int downloadsCount2;



}