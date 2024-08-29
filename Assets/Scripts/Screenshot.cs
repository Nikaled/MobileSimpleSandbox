using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Screenshot : MonoBehaviour
{
    [SerializeField] private GameObject adPanel;
 //   [DllImport("__Internal")]
	//private static extern void DownloadFile(byte[] array, int byteLength, string fileName);

    private int index;
    public void DownloadScreenshot ()
    {
        StartCoroutine(ScreenDo());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Player.instance.AdWarningActive==false)
        {
            DownloadScreenshot();
        }
    }
    IEnumerator ScreenDo()
    {
        index = Geekplay.Instance.PlayerData.downloadsCount2;
        //Выключаем весь UI

        CanvasManager.instance.ShowAllUI(false);
        yield return new WaitForEndOfFrame();
        DateTime date1 = DateTime.Now;
        string MapDate = date1.ToString("HH_mm__dd_MM_yyyy");
        //ДЕЛАЕМ СКРИН НА ВЕБЕ
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
		byte[] textureBytes = texture.EncodeToPNG();
        //DownloadFile(textureBytes, textureBytes.Length, $"screenshot_{MapDate}.png");
        Destroy(texture);

        //ВКЛЮЧАЕМ UI
        CanvasManager.instance.ShowAllUI(true);

        yield return new WaitForSeconds(1f);
        index++;
        Geekplay.Instance.PlayerData.downloadsCount2 = index;
        Debug.Log(Geekplay.Instance.PlayerData.downloadsCount2);
        Geekplay.Instance.Save();
    }
}
