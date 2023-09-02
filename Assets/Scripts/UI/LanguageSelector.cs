using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageSelector : MonoBehaviour
{
    static public string selectedLang = "en";

    public TMP_Text tutoText;
    public Sprite frFlag;
    public Sprite enFlag;
    public Sprite esFlag;
    public Sprite itFlag;
    public Sprite deFlag;
    public Sprite ptFlag;
    public Button button;

    void Update()
    {
        switch (selectedLang)
        {
            case "fr":
                button.image.sprite = frFlag;
                break;
            case "en":
                button.image.sprite = enFlag;
                break;
            case "es":
                button.image.sprite = esFlag;
                break;
            case "it":
                button.image.sprite = itFlag;
                break;
            case "de":
                button.image.sprite = deFlag;
                break;
            case "pt":
                button.image.sprite = ptFlag;
                break;
        }
    }

    public void ChangeLang()
    {
        SaveData saveData = GameObject.FindObjectOfType(typeof(SaveData)) as SaveData;
        TextTranslator textTranslator = GameObject.FindObjectOfType(typeof(TextTranslator)) as TextTranslator;

        string[] languages = { "en", "fr", "es", "it", "de", "pt" };

        // Find the index of the currently selected language in the array
        int currentIndex = 0;

        for (int i = 0; i < languages.Length; i++)
        {
            if (languages[i] == selectedLang)
            {
                currentIndex = i;
                break;
            }
        }

        // Calculate the index of the next language
        int nextIndex = (currentIndex + 1) % languages.Length;

        // Update the selectedLang with the next language in the array
        selectedLang = languages[nextIndex];

        // Save the updated language selection
        saveData.SaveToJson();

        // Localize the text with the new selected language
        textTranslator.Localize(selectedLang);
    }
}
