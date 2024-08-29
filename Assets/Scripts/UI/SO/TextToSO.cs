using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Texts", menuName = "ScriptableObjects/TextsSO", order = 52)]
public class TextToSO : ScriptableObject
{
    public Dictionary<string, string> TextsInObjects;
}
