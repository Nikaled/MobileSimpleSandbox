using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialCanvasManager : MonoBehaviour
{
    public static TutorialCanvasManager instance;
    [SerializeField] GameObject EndTutorialUI;
    [SerializeField] GameObject SaveMapButton;
    private bool IsTutorialActive;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SaveMapButton.SetActive(false);
        CanvasManager.instance.IsTutorial = true;
    }
    public void ShowEndTutorialUI(bool Is)
    {
        IsTutorialActive = Is;
        EndTutorialUI.SetActive(Is);
        CanvasManager.instance.CheckActiveUnlockCursorWindows();
    }
    public void EndTutorialButton()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (Player.instance.AdWarningActive == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowEndTutorialUI(!IsTutorialActive);
        }
    }
}
