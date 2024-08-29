using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MySceneManager : MonoBehaviour
{

    public void LoadSceneBuyIndex(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
