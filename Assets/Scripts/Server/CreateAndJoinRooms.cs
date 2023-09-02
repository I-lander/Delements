using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput, joinInput;
    public TMP_Text errorMessage;

    public void EmptyErrorMessage()
    {
        errorMessage.text = "";
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        errorMessage.text = "";
        if (createInput.text.Length == 0)
        {
            TextTranslator textTranslator = GameObject.FindObjectOfType(typeof(TextTranslator)) as TextTranslator;
            errorMessage.text = textTranslator.GetLocalizedValue("errorCreate", LanguageSelector.selectedLang);
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        errorMessage.text = "";
        if (joinInput.text.Length == 0)
        {
            TextTranslator textTranslator = GameObject.FindObjectOfType(typeof(TextTranslator)) as TextTranslator;
            errorMessage.text = textTranslator.GetLocalizedValue("errorJoin", LanguageSelector.selectedLang);
            return;
        }
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
