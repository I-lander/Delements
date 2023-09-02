using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class Main : MonoBehaviourPun
{

    public ActiveDice activeDice;
    public string GamerColor;
    public GameObject controls;
    public GameObject waitingRoom;
    public GameObject DisconnectedScreen;
    public bool isPaused;

    public int roomStatus = 10;

    DicePivotScript dps;

    public void UpdatePause()
    {
        isPaused = !isPaused;
    }

    void Awake()
    {
        roomStatus = 10;
    }

    void Update()
    {
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;
        if (!Init.isOnline)
        {
            waitingRoom.SetActive(false);
        }
        if (Init.isOnline && PhotonNetwork.CurrentRoom.PlayerCount < 2 && roomStatus == 10)
        {
            waitingRoom.SetActive(true);
        }
        if (Init.isOnline && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            waitingRoom.SetActive(false);
            if (roomStatus == 10)
            {
                roomStatus = 20;
            }
        }
        if (Init.isOnline && PhotonNetwork.CurrentRoom.PlayerCount < 2 && roomStatus == 20)
        {
            DisconnectedScreen.SetActive(true);
        }
    }

    public bool isLocalPlayer()
    {
        if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient &&
            GamerColor == "BlackDice")
        {
            return true;
        }
        if (PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient &&
            GamerColor == "WhiteDice")
        {
            return true;
        }
        return false;

    }
    public void DefineActiveDice(string gameObject, int viewID = 0)
    {
        if (Init.isOnline)
        {
            photonView.RPC("DefineActiveDiceRPC", RpcTarget.All, viewID);
            if (controls.transform.position.x == activeDice.transform.position.x && controls.transform.position.y == activeDice.transform.position.y && controls.activeSelf)
            {
                controls.SetActive(false);
                return;
            }
            controls.transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y, 0);
            controls.SetActive(true);
            Transform parentTransformOnline = controls.transform;
            foreach (Transform child in parentTransformOnline)
            {
                GameObject childObject = child.gameObject;
                childObject.SetActive(true);
                if (childObject.name != "ControlsScript")
                {
                    if (childObject.tag != "JumpButton")
                    {
                        childObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            return;
        }

        activeDice = GameObject.Find(gameObject).GetComponent<ActiveDice>();
        dps.activeDice = activeDice;
        controls.transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y, 0);
        controls.SetActive(true);
        Transform parentTransform = controls.transform;
        foreach (Transform child in parentTransform)
        {
            GameObject childObject = child.gameObject;
            childObject.SetActive(true);
            if (childObject.name != "ControlsScript")
            {
                if (childObject.tag != "JumpButton")
                {
                    childObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
    [PunRPC]
    public void DefineActiveDiceRPC(int viewID)
    {
        PhotonView targetPhotonView = PhotonView.Find(viewID);
        if (targetPhotonView != null)
        {
            activeDice = targetPhotonView.gameObject.GetComponent<ActiveDice>();
            dps.activeDice = activeDice;
        }
    }
    public void DisableActiveDice()
    {
        activeDice = null;
        dps.activeDice = activeDice;
        controls.transform.position = new Vector3(0, 0, 2);
        controls.SetActive(false);
    }

    public void ChangeGamerColor()
    {
        if (Init.isOnline && photonView.IsMine)
        {
            string newColor = GamerColor == "BlackDice" ? "WhiteDice" : "BlackDice";
            photonView.RPC("ChangeGamerColorRPC", RpcTarget.All, newColor);
        }
        else
        {
            GamerColor = GamerColor == "BlackDice" ? "WhiteDice" : "BlackDice";
        }
    }

    [PunRPC]
    void ChangeGamerColorRPC(string newColor)
    {
        GamerColor = newColor;
    }
}
