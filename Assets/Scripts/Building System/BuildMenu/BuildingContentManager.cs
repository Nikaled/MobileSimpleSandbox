using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContentManager : MonoBehaviour
{
    [SerializeField] public BuildingCell[] CellsInGrid;
    [SerializeField] CellNamesSO cellNames;

    private void Start()
    {
        for (int i = 0; i < CellsInGrid.Length; i++)
        {
            CellsInGrid[i].ItemOpened += SaveCellsUnlockState;
        }
        //SendNamesToSO();
    }
    public void SendNamesToSO()
    {
        cellNames.names = new string[CellsInGrid.Length];
        for (int i = 0; i < CellsInGrid.Length; i++)
        {
            cellNames.names[i] = CellsInGrid[i].ObjectNameText.text;
        }
    }

    public void SaveCellsUnlockState()
    {
        List<bool> CellsState = new();
        for (int i = 0; i < CellsInGrid.Length; i++)
        {
            CellsState.Add(CellsInGrid[i].IsOpened);
        }
        SaverBuildingMenu.instance.SaveItemsState(CellsState, this);
    }
}
