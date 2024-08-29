using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCell : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    public string ObjectName = "------";
    public TextMeshProUGUI ObjectNameText; 
    public RawImage ObjectScreen;
    public int CoinPrice;
    public TextMeshProUGUI PriceText;
    public GameObject priceObj;
    public bool IsOpened;

    public  Action ItemOpened;
    private void Start()
    {
        InitializeButtons();
        if(CoinPrice > 0)
        {
        PriceText.text = CoinPrice.ToString();
        }
        else
        {
            priceObj.gameObject.SetActive(false);
        }
        
    }
    private void InitializeButtons()
    {
        if(gameObject.GetComponent<Button>() != null)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { SendPrefabToManager(); });
        }
    }
    protected virtual IEnumerator SendPrefabToManagerCor()
    {
        yield return new WaitForSeconds(0.1f);
        if (IsOpened)
        {
            BuildingManager.instance.ActivateBuildingButton(false);
            CanvasManager.instance.ShowBuildingMenu(false);
            BuildingManager.instance.SetBuildingObject(objectPrefab);
        }
        else
        {
            if (Geekplay.Instance.PlayerData.Coins >= CoinPrice)
            {
                IsOpened = true;
                BuildingManager.instance.ActivateBuildingButton(false);
                CanvasManager.instance.ShowBuildingMenu(false);
                Geekplay.Instance.PlayerData.Coins -= CoinPrice;
                BuildingManager.instance.SetBuildingObject(objectPrefab);
                OpenedInvoker();
                priceObj.gameObject.SetActive(false);
            }
            else
            {
            //    CanvasManager.instance.ShowInAppShop(true);
            }
        }
    }
    public void SendPrefabToManager()
    {
        StartCoroutine(SendPrefabToManagerCor()); 
    }
    public void CheckPriceVisible()
    {
        if (IsOpened)
        {
            priceObj.gameObject.SetActive(false);
        }
    }
    protected virtual void OpenedInvoker()
    {
        ItemOpened?.Invoke();
    }
}
