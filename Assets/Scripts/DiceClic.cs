using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class DiceClic : MonoBehaviour
{
    Main main;
    DicePivotScript dps;

    private void Awake()
    {
        main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;
    }
    void Update()
    {
        this.transform.position = new Vector2(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y);
        this.transform.eulerAngles = Vector3.zero;
    }

    void OnMouseDown()
    {
        if (Init.isOnline && !main.isLocalPlayer())
        {
            return;
        }
        if (dps.isRotating || main.isPaused)
        {
            return;
        }
        if (!Init.isOnline && main.activeDice && this.transform.parent.name == main.activeDice.name && !dps.isBattling)
        {
            main.DisableActiveDice();
            return;
        }
        if (!Init.isOnline && main.GamerColor == this.transform.parent.tag && !dps.isBattling)
        {
            main.DefineActiveDice(transform.parent.gameObject.name);
        }

        if (Init.isOnline && PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient &&
        main.GamerColor == "BlackDice" &&
        this.transform.parent.tag == "BlackDice" && !dps.isBattling)
        {
            PhotonView photonView = transform.parent.gameObject.GetComponent<PhotonView>();
            int viewID = photonView.ViewID;
            main.DefineActiveDice("0", viewID);
        }
        if (Init.isOnline && PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient &&
        main.GamerColor == "WhiteDice" &&
        this.transform.parent.tag == "WhiteDice" && !dps.isBattling)
        {
            PhotonView photonView = transform.parent.gameObject.GetComponent<PhotonView>();
            int viewID = photonView.ViewID;
            main.DefineActiveDice("0", viewID);
        }
    }
}
