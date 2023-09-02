using UnityEngine;

public class SaveData : MonoBehaviour
{
    void Awake()
    {
        LoadFromJson();
    }

    public void SaveToJson()
    {
        GameState gameState = new GameState();
        gameState.isMute = MuteButton.isMute;
        gameState.selectedLang = LanguageSelector.selectedLang;
        string gameStateJson = JsonUtility.ToJson(gameState);
        string filePath = Application.persistentDataPath + "/DataState.json";
        System.IO.File.WriteAllText(filePath, gameStateJson);
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/DataState.json";
        string data = System.IO.File.ReadAllText(filePath);
        GameState gameState = JsonUtility.FromJson<GameState>(data);
        if (gameState == null || gameState.selectedLang.Length > 2)
        {
            return;
        }
        MuteButton.isMute = gameState.isMute;
        LanguageSelector.selectedLang = gameState.selectedLang;
    }
}

[System.Serializable]
public class GameState
{
    public bool isMute;
    public string selectedLang;

    public GameState()
    {
        isMute = false;
        selectedLang = "";
    }
}
