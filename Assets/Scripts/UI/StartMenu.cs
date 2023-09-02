using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_Text VersionMessage;

    void Start()
    {
        if (VersionMessage)
        {
            VersionMessage.text = "version " + Application.version;
        }

        // StartCoroutine(GetWebData("http://localhost:8000/test/", "123"));
    }

    IEnumerator GetWebData(string adress, string id)
    {
        UnityWebRequest www = UnityWebRequest.Get(adress + id);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("ERROR " + www.error);
        }
        

        else
        {
            Debug.Log(www.downloadHandler.text);
            Data data = JsonUtility.FromJson<Data>(www.downloadHandler.text);
            print(data.testId);
        }
    }

    [System.Serializable]
    public class Data
    {
        public string testId;
        public string testName;
        public int testOther;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Init.isOnline = false;
    }

    public void OnlineGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene("Loading");
        Init.isOnline = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene("StartScreen");
    }
}
