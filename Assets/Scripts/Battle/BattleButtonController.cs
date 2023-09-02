using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BattleButtonController : MonoBehaviourPun
{
    DicePivotScript dps;
    BattleMecanics battleMecanics;
    Init init;
    Main main;

    public Collider2D ennemy;

    public bool isFighting = false;
    private bool hasCollision = false;

    public float cooldownTime = 0.5f;
    public bool isReady = false;
    bool isClicked = false;


    IEnumerator SetReadyAfterDelay()
    {
        yield return new WaitForSeconds(cooldownTime);
        isReady = true;
    }

    void OnEnable()
    {
        if (Init.isOnline)
        {
            main = GameObject.FindObjectOfType(typeof(Main)) as Main;
            getOwnership();
        }
        isReady = false;
        isClicked = false;
        StartCoroutine(SetReadyAfterDelay());
    }

    void OnDisable()
    {
        isReady = false;
        isClicked = false;
    }

    void Start()
    {
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;
        main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        battleMecanics = GameObject.FindObjectOfType(typeof(BattleMecanics)) as BattleMecanics;
    }

    void Update()
    {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        Vector2 point = boxCollider.bounds.center;
        Vector2 size = boxCollider.bounds.size;
        boxCollider.enabled = false;
        Collider2D collision = Physics2D.OverlapBox(point, size, 0f);
        boxCollider.enabled = true;
        if (!isFighting && collision != null && collision.tag != "Wall")
        {
            isFighting = battleMecanics.ManageBattleControlsEnterCollision(collision);
        }
        if (isFighting)
        {
            ennemy = collision;
            hasCollision = true;
        }
        if (ennemy == null)
        {
            hasCollision = false;
            isFighting = false;
        }
        if (hasCollision)
        {
            gameObject.SetActive(true);
            if (!Init.isOnline || Init.isOnline && PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient &&
            main.GamerColor == "BlackDice" &&
            dps.activeDice.tag == "BlackDice")
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (!Init.isOnline || Init.isOnline && PhotonNetwork.LocalPlayer != PhotonNetwork.MasterClient &&
            main.GamerColor == "WhiteDice" &&
            dps.activeDice.tag == "WhiteDice")
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            dps.countEndTurn++;
            gameObject.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        hasCollision = false;
        ennemy = null;
        isFighting = false;
    }

    void OnMouseDown()
    {
        if (main.isPaused || !isReady || isClicked || (Init.isOnline && !main.isLocalPlayer()) || (Init.isOnline && !photonView.IsMine))
        {
            return;
        }

        isClicked = true;
        if (isFighting)
        {
            bool isWinning = battleMecanics.ManageBattle(ennemy);
            if (isWinning && !Init.isOnline)
            {
                battleMecanics.ManageWinning(ennemy, this.gameObject.name);
            }
            if (isWinning && Init.isOnline)
            {
                battleMecanics.CallManageWinning(ennemy, this.gameObject.name);
            }

        }
        if (!Init.isOnline)
        {
            dps.isBattled = true;
        }
    }

    [PunRPC]
    void updateIsBattledRPC()
    {
        dps.isBattled = true;
    }

    void getOwnership()
    {
        if (main.isLocalPlayer() && !photonView.IsMine)
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
