using UnityEngine;
using Photon.Pun;

public class Init : MonoBehaviour
{
    public GameObject blackDicePrefab;
    public GameObject whiteDicePrefab;
    public GameObject blackDicePrefabOffline;
    public GameObject whiteDicePrefabOffline;
    public GameObject controls;
    public GameObject battleControls;

    public GameObject[] diceAray;

    static public bool isOnline = false;
    public int blackToDefeat;
    public int whiteToDefeat;

    int numberOfDices = 8;
    int diceArayIndex = 0;
    int spacing = 4;
    bool secoundLine = false;

    float initX = -5;
    float initY = -5;
    float initZ = -0.8f;

    void Start()
    {
        diceAray = new GameObject[16];
        InitBoard();
    }

    public void InitBoard()
    {
        GameObject dice = null;
        for (int i = 0; i <= numberOfDices - 1; i++)
        {
            if (i > 3 && !secoundLine)
            {
                initY = -7;
                initX = -23;
                secoundLine = true;
            };
            if (!isOnline)
            {
                dice = Instantiate(blackDicePrefabOffline);
                diceAray[diceArayIndex] = dice;
                diceArayIndex++;
                Vector3 rotation = transform.localEulerAngles;
                rotation = RandomizeRotation(rotation);
                dice.transform.localEulerAngles = rotation;
                dice.transform.position = new Vector3(initX + i * spacing, initY, initZ);
                dice.name = "BlackDice" + i;
                dice.SetActive(true);
            }
            if (isOnline && PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
            {
                dice = PhotonNetwork.Instantiate(blackDicePrefab.name, blackDicePrefab.transform.position, blackDicePrefab.transform.rotation);
                PhotonView photonView = dice.GetComponent<PhotonView>();
                photonView.ViewID = i + 100;
                diceAray[diceArayIndex] = dice;
                diceArayIndex++;
                Vector3 rotation = transform.localEulerAngles;
                rotation = RandomizeRotation(rotation);
                dice.transform.localEulerAngles = rotation;
                dice.transform.position = new Vector3(initX + i * spacing, initY, initZ);
                photonView.Owner.CustomProperties["diceName"] = "BlackDice" + i;
                dice.SetActive(true);
            }

        }

        secoundLine = false;
        initX = -5;
        initY = -5;

        for (int i = 0; i <= numberOfDices - 1; i++)
        {
            if (i > 3 && !secoundLine)
            {
                initY = -7;
                initX = -23;
                secoundLine = true;
            };
            if (!isOnline)
            {
                dice = Instantiate(whiteDicePrefabOffline);
                diceAray[diceArayIndex] = dice;
                diceArayIndex++;
                Vector3 rotation = transform.localEulerAngles;
                rotation = RandomizeRotation(rotation);
                dice.transform.localEulerAngles = rotation;
                dice.transform.position = new Vector3(initX + i * spacing, initY * -1, initZ);
                dice.name = "WhiteDice" + i;
                dice.SetActive(true);
            }
            if (isOnline && PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
            {
                dice = PhotonNetwork.Instantiate(whiteDicePrefab.name, whiteDicePrefab.transform.position, whiteDicePrefab.transform.rotation);
                PhotonView photonView = dice.GetComponent<PhotonView>();
                photonView.ViewID = i + 200;
                diceAray[diceArayIndex] = dice;
                diceArayIndex++;
                Vector3 rotation = transform.localEulerAngles;
                rotation = RandomizeRotation(rotation);
                dice.transform.localEulerAngles = rotation;
                dice.transform.position = new Vector3(initX + i * spacing, initY * -1, initZ);
                photonView.Owner.CustomProperties["diceName"] = "WhiteDice" + i;
                dice.SetActive(true);
            }

        }
        secoundLine = false;
        initX = -5;
        initY = -5;

    }

    public void DestroyAllDice()
    {
        for (int i = 0; i < diceAray.Length; i++)
        {
            if (!Init.isOnline)
            {
                Destroy(diceAray[i]);
            }
            if (Init.isOnline)
            {
                Destroy(diceAray[i]);
            }
        }
        diceArayIndex = 0;
    }

    public void ResetGame()
    {
        DestroyAllDice();
        InitBoard();
        Main main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        BattleMecanics battleMecanics = GameObject.FindObjectOfType(typeof(BattleMecanics)) as BattleMecanics;
        battleMecanics.particlesTriggered = false;
        main.activeDice = null;
        main.GamerColor = "BlackDice";
        battleMecanics.partyEnded = false;
        controls.SetActive(false);
        battleControls.SetActive(false);
        blackToDefeat = 8;
        whiteToDefeat = 8;
    }

    Vector3 RandomizeRotation(Vector3 rotation)
    {
        int randomX = Random.Range(0, 2);
        int randomY = Random.Range(0, 2);
        int randomZ = Random.Range(0, 2);
        if (randomX > 0)
        {
            rotation.x = 90;
            if (Random.Range(0, 2) > 0)
            {
                rotation.x = rotation.x * -1;
            }
        }
        if (randomY > 0)
        {
            rotation.y = 90;
            if (Random.Range(0, 2) > 0)
            {
                rotation.y = rotation.y * -1;
            }
        }
        if (randomZ > 0)
        {
            rotation.z = 90;
            if (Random.Range(0, 2) > 0)
            {
                rotation.z = rotation.z * -1;
            }
        }

        return rotation;
    }
}
