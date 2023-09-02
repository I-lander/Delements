using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Background : MonoBehaviour
{
    Camera cameraRef;
    Main main;
    // Start is called before the first frame update
    void Start()
    {
        if (Init.isOnline && PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient)
        {
            Vector3 rotation = transform.localEulerAngles;
            rotation.z = 180;
            transform.localEulerAngles = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraRef = Camera.main;
        main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        if (main.GamerColor == "BlackDice")
        {
            cameraRef.backgroundColor = new Color(0.80f, 0.82f, 0.91f, 1);

        }
        if (main.GamerColor == "WhiteDice")
        {
            cameraRef.backgroundColor = new Color(0.91f, 0.89f, 0.80f, 1);
        }
    }
}
