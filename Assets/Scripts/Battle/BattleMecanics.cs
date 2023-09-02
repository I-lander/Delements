using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class BattleMecanics : MonoBehaviourPun
{
    DicePivotScript dps;
    Init init;

    public GameObject battleControls;
    public GameObject winningMenu;
    public ParticleSystem ps1, ps2, explosionE, explosionF, explosionW, explosionT;
    public TMP_Text winningText;
    public AudioSource eatDiceSound;

    public bool partyEnded;
    public bool particlesTriggered;

    private void Awake()
    {
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;
        init = GameObject.FindObjectOfType(typeof(Init)) as Init;
    }

    void Update()
    {
        if (Init.isOnline)
        {
            string winText;
            if (PhotonNetwork.IsMasterClient)
            {
                if (init.blackToDefeat <= 0 && winningMenu)
                {
                    winText = updateWinTextOnline();
                    winningText.text = winText;
                    photonView.RPC("EndPartyRPC", RpcTarget.AllBuffered);
                }
                if (init.whiteToDefeat <= 0 && winningMenu)
                {
                    winText = updateWinTextOnline();
                    winningText.text = winText;
                    photonView.RPC("EndPartyRPC", RpcTarget.AllBuffered);
                }
            }
            else
            {
                if (init.blackToDefeat <= 0 && winningMenu)
                {
                    winText = updateWinTextOnline();
                    winningText.text = winText;
                }
                if (init.whiteToDefeat <= 0 && winningMenu)
                {
                    winText = updateWinTextOnline();
                    winningText.text = winText;
                }
            }
        }
        else
        {
            TextTranslator textTranslator = GameObject.FindObjectOfType(typeof(TextTranslator)) as TextTranslator;
            if (init.blackToDefeat <= 0 && winningMenu)
            {
                if (!particlesTriggered)
                {
                    ps1.Play();
                    ps2.Play();
                    particlesTriggered = true;
                }
                partyEnded = true;
                winningMenu.SetActive(true);
                winningText.text = textTranslator.GetLocalizedValue("whiteWin", LanguageSelector.selectedLang);
            }
            if (init.whiteToDefeat <= 0 && winningMenu)
            {
                if (!particlesTriggered)
                {
                    ps1.Play();
                    ps2.Play();
                    particlesTriggered = true;
                }
                partyEnded = true;
                winningMenu.SetActive(true);
                winningText.text = textTranslator.GetLocalizedValue("blueWin", LanguageSelector.selectedLang);
            }
        }
    }

    string updateWinTextOnline()
    {
        TextTranslator textTranslator = GameObject.FindObjectOfType(typeof(TextTranslator)) as TextTranslator;
        if (init.blackToDefeat <= 0 && winningMenu)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
            {
                return textTranslator.GetLocalizedValue("lose", LanguageSelector.selectedLang);
            }
            else
            {
                return textTranslator.GetLocalizedValue("win", LanguageSelector.selectedLang);
            }
        }
        if (init.whiteToDefeat <= 0 && winningMenu)
        {
            if (PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient)
            {
                return textTranslator.GetLocalizedValue("lose", LanguageSelector.selectedLang);
            }
            else
            {
                return textTranslator.GetLocalizedValue("win", LanguageSelector.selectedLang);
            }
        }
        else
        {
            return "";
        }
    }

    [PunRPC]
    void EndPartyRPC()
    {
        if (!particlesTriggered)
        {
            ps1.Play();
            ps2.Play();
            particlesTriggered = true;
        }
        partyEnded = true;
        winningMenu.SetActive(true);
    }

    public bool ManageBattle(Collider2D other)
    {
        string ennemy = other.transform.parent.GetComponent<ActiveDice>().faceUp;
        string fighter = dps.activeDice.GetComponent<ActiveDice>().faceUp;

        if (fighter == "Thunder" || ennemy == "Thunder")
        {
            return true;
        }
        if (fighter == "Water" && ennemy == "Fire")
        {
            return true;
        }
        if (fighter == "Fire" && ennemy == "Earth")
        {
            return true;
        }
        if (fighter == "Earth" && ennemy == "Water")
        {
            return true;
        }

        return false;
    }

    public bool testForBattle(Collider2D other)
    {
        Main main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        if (other.tag == "Dice" && other.transform.parent.tag != main.activeDice.tag)
        {
            return true;
        }
        return false;
    }

    public bool ManageBattleControlsEnterCollision(Collider2D other)
    {
        return testForBattle(other) && ManageBattle(other);
    }

    public bool ManageBattleControlsExitCollision(Collider2D other)
    {
        if (other.tag == "Dice")
        {
            return false;
        }
        return true;
    }

    public bool TestForEnnemies()
    {
        Transform parentTransform = battleControls.transform;
        foreach (Transform child in parentTransform)
        {
            GameObject childObject = child.gameObject;
            if (childObject.activeSelf == true)
            {
                return true;
            };
        }
        return false;
    }

    public void ManageWinning(Collider2D ennemy, string name)
    {
        ParticleSystem explosion = null;
        Transform ennemyParent = ennemy.gameObject.transform.parent;
        if (ennemyParent.tag == "BlackDice")
        {
            init.blackToDefeat -= 1;
        }
        if (ennemyParent.tag == "WhiteDice")
        {
            init.whiteToDefeat -= 1;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Water")
        {
            explosion = explosionW;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Fire")
        {
            explosion = explosionF;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Earth")
        {
            explosion = explosionE;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Thunder")
        {
            explosion = explosionT;
        }
        explosion.transform.position = new Vector3(ennemyParent.gameObject.transform.position.x, ennemyParent.gameObject.transform.position.y, ennemyParent.gameObject.transform.position.z);
        explosion.Play();
        if (!MuteButton.isMute)
        {
            eatDiceSound.Play();
        }
        if (Init.isOnline)
        {
            PhotonView pv = ennemyParent.gameObject.GetComponent<PhotonView>();
            if (pv != null)
            {
                if (pv.IsMine || PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(pv.gameObject);
                }
                else
                {
                    photonView.RPC("RequestDestroyObjectRPC", RpcTarget.MasterClient, pv.ViewID);
                }
            }
        }
        ennemyParent.gameObject.SetActive(false);
        if (name == "battleUp")
        {
            dps.moveUp();
        }
        if (name == "battleDown")
        {
            dps.moveDown();
        }
        if (name == "battleLeft")
        {
            dps.moveLeft();
        }
        if (name == "battleRight")
        {
            dps.moveRight();
        }
        dps.isBattling = false;
    }

    [PunRPC]
    void RequestDestroyObjectRPC(int photonViewId)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView pv = PhotonView.Find(photonViewId);
            if (pv != null)
            {
                PhotonNetwork.Destroy(pv.gameObject);
            }
        }
    }

    [PunRPC]
    public void ManageWinningRPC(string enemyName, string name)
    {
        GameObject enemyGameObject = PhotonView.Find(int.Parse(enemyName)).gameObject;
        Collider2D enemy = enemyGameObject.GetComponent<Collider2D>();

        ParticleSystem explosion = null;
        Transform enemyParent = enemy.transform.parent;
        if (enemyParent.tag == "BlackDice")
        {
            init.blackToDefeat -= 1;
        }
        if (enemyParent.tag == "WhiteDice")
        {
            init.whiteToDefeat -= 1;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Water")
        {
            explosion = explosionW;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Fire")
        {
            explosion = explosionF;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Earth")
        {
            explosion = explosionE;
        }
        if (dps.activeDice.GetComponent<ActiveDice>().faceUp == "Thunder")
        {
            explosion = explosionT;
        }

        explosion.transform.position = new Vector3(enemyParent.position.x, enemyParent.position.y, enemyParent.position.z);
        explosion.Play();
        if (!MuteButton.isMute)
        {
            eatDiceSound.Play();
        }
        enemyParent.gameObject.SetActive(false);
    }

    public void CallManageWinning(Collider2D enemy, string name)
    {
        photonView.RPC("ManageWinningRPC", RpcTarget.All, enemy.GetComponent<PhotonView>().ViewID.ToString(), name);
        if (!dps.isRotating && !dps.isBattled)
        {
            if (name == "battleUp")
            {
                dps.moveUp();
            }
            if (name == "battleDown")
            {
                dps.moveDown();
            }
            if (name == "battleLeft")
            {
                dps.moveLeft();
            }
            if (name == "battleRight")
            {
                dps.moveRight();
            }
        }
        photonView.RPC("UpdateStatusRPC", RpcTarget.All);
    }

    [PunRPC]
    void UpdateStatusRPC()
    {
        dps.isBattling = false;
        dps.isBattled = true;
    }

}
