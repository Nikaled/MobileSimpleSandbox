using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarButtons : MonoBehaviour
{
    public Button GoLeft;
    public Button GoRight;
    public Button GoForward;
    public Button GoBack;
    public Button DownEngine;
    public Button GetOutButton;
    public Button ShootButton;
    public static CarButtons instance;
    private void Awake()
    {
        instance = this;
    }
}
