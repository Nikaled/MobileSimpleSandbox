using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] GameObject[] ContentPanels;
    [SerializeField] Button[] LeftPanelButtons;
    [SerializeField] Sprite InactiveButtonSprite;
    [SerializeField] Sprite ActiveButtonSprite;
    public void HideAllOtherPanels()
    {
        for (int i = 0; i < ContentPanels.Length; i++)
        {
            ContentPanels[i].SetActive(false);
            LeftPanelButtons[i].image.sprite = InactiveButtonSprite;
        }
    }
    private void Start()
    {
        for (int i = 0; i < LeftPanelButtons.Length; i++)
        {
            Button but = LeftPanelButtons[i];
            LeftPanelButtons[i].onClick.AddListener(delegate { ShowPanelAtIndex(but); });
        }
    }
    public void ShowPanelAtIndex(Button buttonToFindIndex)
    {
        int index = 0;
        for (int i = 0; i < LeftPanelButtons.Length; i++)
        {
            if(buttonToFindIndex == LeftPanelButtons[i])
            {
                index = i;
            }
        }
        HideAllOtherPanels();
        LeftPanelButtons[index].image.sprite = ActiveButtonSprite;
        ContentPanels[index].SetActive(true);
    }
}
