using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    static public bool isMute = false;
    public Sprite soundIcon, soundIconMuted;
    public Button button;

    void Update()
    {
        if (isMute)
        {
            button.image.sprite = soundIconMuted;
        }
        else
        {
            button.image.sprite = soundIcon;
        }
    }


    public void ActiveMute()
    {
        isMute = !isMute;
        SaveData saveData = GameObject.FindObjectOfType(typeof(SaveData)) as SaveData;
        saveData.SaveToJson();
    }
}
