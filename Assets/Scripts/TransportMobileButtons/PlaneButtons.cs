using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneButtons : MonoBehaviour
{
    public Button RollLeft;
    public Button RollRight;
    public Button GoLeft;
    public Button GoRight;
    public Button GoForward;
    public Button GoBack;
    public Button UpEngine;
    public Button DownEngine;
    public Button GetOutButton;
    public static PlaneButtons instance;
    private void Awake()
    {
        instance = this;
    }
}
