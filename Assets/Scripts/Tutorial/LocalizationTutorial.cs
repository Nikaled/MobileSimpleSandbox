using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationTutorial : MonoBehaviour
{
    [Header("World Canvas")]
    [SerializeField] TextMeshProUGUI Phrase1;
    [SerializeField] TextMeshProUGUI Phrase2;
    [SerializeField] TextMeshProUGUI Phrase3;
    [SerializeField] TextMeshProUGUI Phrase4;
    [SerializeField] TextMeshProUGUI Phrase5;
    [SerializeField] TextMeshProUGUI Phrase6;
    [SerializeField] TextMeshProUGUI PhraseRotateDeleting;
    [Header("EndTutorialUI")]
    [SerializeField] TextMeshProUGUI EndAsk;
    [SerializeField] TextMeshProUGUI Decline;
    [SerializeField] TextMeshProUGUI Confirm;
    private void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            if (Geekplay.Instance.language == "ru")
            {
                RuMobileLocalization();
            }
            if (Geekplay.Instance.language == "en")
            {
                EnMobileLocalization();
            }
            if (Geekplay.Instance.language == "tr")
            {
                TrMobileLocalization();
            }
        }
        else
        {
            if (Geekplay.Instance.language == "ru")
            {
                RuPCLocalization();
            }
            if (Geekplay.Instance.language == "en")
            {
                EnPCLocalization();
            }
            if (Geekplay.Instance.language == "tr")
            {
                TrPCLocalization();
            }
        }
    }
    private void RuPCLocalization()
    {
        Phrase1.text = "Для постройки объектов используй клавишу <color=orange>[B]</color>";
        Phrase2.text = "Нажимай цифры <color=orange> 1-5 </color> чтобы выбрать оружие и <color=orange> [ЛКМ] </color> чтобы использовать!";
        Phrase3.text = "Создавай животных с помощью <color=orange> [B] </color> и седлай их!";
        Phrase4.text = "Садись в машину и исследуй мир!";
        Phrase5.text = "В основной игре тебя ждут танки, самолеты и многое другое! Вперед!";
        Phrase6.text = "Войди сюда, чтобы завершить обучение!";
        PhraseRotateDeleting.text = "Ты можешь изменять построенный объект, если нажмешь <color=orange>[M]</color> и кликнешь на него!";
        EndAsk.text = "Хотите завершить обучение?";
        Decline.text = "НЕТ";
        Confirm.text = "ДА";
    }
    private void RuMobileLocalization()
    {
        Phrase1.text = "Для постройки объектов используй клавишу <color=orange>[B]</color>";
        Phrase2.text = "Нажимай цифры <color=orange> 1-5 </color> чтобы выбрать оружие и <color=orange> [ЛКМ] </color> чтобы использовать!";
        Phrase3.text = "Создавай животных с помощью <color=orange> [B] </color> и седлай их!";
        Phrase4.text = "Садись в машину и исследуй мир!";
        Phrase5.text = "В основной игре тебя ждут танки, самолеты и многое другое! Вперед!";
        Phrase6.text = "Войди сюда, чтобы завершить обучение!";
        PhraseRotateDeleting.text = "Ты можешь изменять построенный объект, если нажмешь <color=orange>[M]</color> и кликнешь на него!";
        EndAsk.text = "Хотите завершить обучение?";
        Decline.text = "НЕТ";
        Confirm.text = "ДА";
    }
    private void EnPCLocalization() { }
    private void EnMobileLocalization() { }
    private void TrPCLocalization() { }
    private void TrMobileLocalization() { }
}