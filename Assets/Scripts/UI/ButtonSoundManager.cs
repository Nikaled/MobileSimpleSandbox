using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public static ButtonSoundManager instance;
    [SerializeField] AudioSource SoundOnClickButton;
    void Start()
    {
        instance = this;
    }
    public void OnButtonClickSound()
    {
        Debug.Log("Sound plays");
        SoundOnClickButton.Play();
    }
}
