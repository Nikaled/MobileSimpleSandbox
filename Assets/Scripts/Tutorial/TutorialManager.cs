using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] PC_TutorialTexts;
    [SerializeField] GameObject[] Mobile_TutorialTexts;

    private void Start()
    {
        bool IsMobile = Geekplay.Instance.mobile;
        for (int i = 0; i < PC_TutorialTexts.Length; i++)
        {
            PC_TutorialTexts[i].SetActive(!IsMobile);
        }
        for (int i = 0; i < Mobile_TutorialTexts.Length; i++)
        {
            Mobile_TutorialTexts[i].SetActive(IsMobile);
        }
    }
}
