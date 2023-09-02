using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DicePivotScript : MonoBehaviourPun
{
    Main main;
    BattleMecanics battleMecanics;

    public GameObject controls;
    public GameObject battleControls;
    public ActiveDice activeDice;

    public AudioSource moveDice;

    public int countEndTurn = 0;

    public bool isRotating = false;
    public bool isJumping = false;
    public bool isJumping3 = false;
    private bool lastIsRotating = false;
    public bool isBattling = false;
    public bool isBattled = false;
    float rotatingAngle = 0;
    float rotatingSpeed = 250;
    bool movingUp = false;
    bool movingDown = false;
    bool movingLeft = false;
    bool movingRight = false;

    public float cooldownTime;
    public bool isReady = false;

    IEnumerator SetReadyAfterDelay()
    {
        cooldownTime = PhotonNetwork.GetPing() + 50;
        yield return new WaitForSeconds(cooldownTime/1000);
        StartBattle();
    }

    void Awake()
    {
        main = GameObject.FindObjectOfType(typeof(Main)) as Main;
        battleMecanics = GameObject.FindObjectOfType(typeof(BattleMecanics)) as BattleMecanics;
    }
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        float jumpAngle;
        if (isJumping || isJumping3)
        {
            jumpAngle = 180;
        }
        else
        {
            jumpAngle = 90;
        }
        if (movingUp)
        {
            if (rotatingAngle <= jumpAngle && isRotating)
            {
                activeDice.transform.SetParent(this.transform);
                transform.rotation = Quaternion.AngleAxis(rotatingAngle, Vector3.right);
                rotatingAngle += rotatingSpeed * Time.deltaTime;
            }
            else
            {
                rotatingAngle = 0;
                isRotating = false;
                isJumping = false;
                isJumping3 = false;
                movingUp = false;
                transform.rotation = Quaternion.AngleAxis(jumpAngle, Vector3.right);
                transform.DetachChildren();
                transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
                transform.position = new Vector3(0, 0, 0);
                EndRotation();
            }
        }
        if (movingDown)
        {
            if (rotatingAngle <= jumpAngle && isRotating)
            {
                activeDice.transform.SetParent(this.transform);
                transform.rotation = Quaternion.AngleAxis(-rotatingAngle, Vector3.right);
                rotatingAngle += rotatingSpeed * Time.deltaTime;
            }
            else
            {
                rotatingAngle = 0;
                isRotating = false;
                isJumping = false;
                isJumping3 = false;
                movingDown = false;
                transform.rotation = Quaternion.AngleAxis(-jumpAngle, Vector3.right);
                transform.DetachChildren();
                transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
                transform.position = new Vector3(0, 0, 0);
                EndRotation();
            }
        }
        if (movingLeft)
        {
            if (rotatingAngle <= jumpAngle && isRotating)
            {
                activeDice.transform.SetParent(this.transform);
                transform.rotation = Quaternion.AngleAxis(rotatingAngle, Vector3.up);
                rotatingAngle += rotatingSpeed * Time.deltaTime;
            }
            else
            {
                rotatingAngle = 0;
                isRotating = false;
                isJumping = false;
                isJumping3 = false;
                movingLeft = false;
                transform.rotation = Quaternion.AngleAxis(jumpAngle, Vector3.up);
                transform.DetachChildren();
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                transform.position = new Vector3(0, 0, 0);
                EndRotation();
            }
        }
        if (movingRight)
        {
            if (rotatingAngle <= jumpAngle && isRotating)
            {
                activeDice.transform.SetParent(this.transform);
                transform.rotation = Quaternion.AngleAxis(-rotatingAngle, Vector3.up);
                rotatingAngle += rotatingSpeed * Time.deltaTime;
            }
            else
            {
                rotatingAngle = 0;
                isRotating = false;
                isJumping = false;
                isJumping3 = false;
                movingRight = false;
                transform.rotation = Quaternion.AngleAxis(-jumpAngle, Vector3.up);
                transform.DetachChildren();
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                transform.position = new Vector3(0, 0, 0);
                EndRotation();
            }
        }
        if (countEndTurn >= 4 && isBattling && !isBattled)
        {
            if (!Init.isOnline)
            {
                isBattling = false;
                isBattled = true;
                countEndTurn = 0;
                main.ChangeGamerColor();
            }
            if (Init.isOnline)
            {
                photonView.RPC("SyncEndTurnRPC", RpcTarget.All);
            }
        }
        if (lastIsRotating != isRotating && Init.isOnline)
        {
            photonView.RPC("SyncDiceRotation", RpcTarget.Others, isRotating);
            lastIsRotating = isRotating;
        }
    }

    void EndRotation()
    {
        if (!isBattled)
        {
            isBattling = true;
            StartCoroutine(SetReadyAfterDelay());
        }
        else
        {
            globalMecanics();
        }
    }

    [PunRPC]
    void SyncEndTurnRPC()
    {
        isBattling = false;
        isBattled = true;
        countEndTurn = 0;
        main.ChangeGamerColor();
    }

    public void globalMecanics()
    {
        if (Init.isOnline)
        {
            photonView.RPC("GlobalMecanicsRTC", RpcTarget.All);
        }
        if (!Init.isOnline)
        {
            controls.SetActive(false);
            battleControls.SetActive(false);
            countEndTurn = 0;
            if (!battleMecanics.partyEnded)
            {
                main.ChangeGamerColor();
            }
        }
    }

    public void moveUp()
    {
        if (!MuteButton.isMute)
        {
            moveDice.Play();
        }
        if (Init.isOnline && !isRotating)
        {
            photonView.RPC("moveUpRPC", RpcTarget.All);
            return;
        }
        float transformXY;
        if (isJumping)
        {
            transformXY = 2;
        }
        else if (isJumping3)
        {
            transformXY = 3;
        }
        else
        {
            transformXY = 1;
        }
        float transformZ;
        if (isJumping || isJumping3)
        {
            transformZ = -0.8f;
        }
        else
        {
            transformZ = 0.2f;
        }
        transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y + transformXY, transformZ);
        movingUp = true;
        isRotating = true;
    }
    [PunRPC]
    public void moveUpRPC()
    {
        if (!isRotating)
        {
            if (!MuteButton.isMute)
            {
                moveDice.Play();
            }
            float transformXY;
            if (isJumping)
            {
                transformXY = 2;
            }
            else if (isJumping3)
            {
                transformXY = 3;
            }
            else
            {
                transformXY = 1;
            }
            float transformZ;
            if (isJumping || isJumping3)
            {
                transformZ = -0.8f;
            }
            else
            {
                transformZ = 0.2f;
            }
            transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y + transformXY, transformZ);
            movingUp = true;
            isRotating = true;
        }
    }
    public void moveDown()
    {
        if (!MuteButton.isMute)
        {
            moveDice.Play();
        }
        if (Init.isOnline && !isRotating)
        {
            photonView.RPC("moveDownRPC", RpcTarget.All);
            return;
        }
        float transformXY;
        if (isJumping)
        {
            transformXY = 2;
        }
        else if (isJumping3)
        {
            transformXY = 3;
        }
        else
        {
            transformXY = 1;
        }
        float transformZ;
        if (isJumping || isJumping3)
        {
            transformZ = -0.8f;
        }
        else
        {
            transformZ = 0.2f;
        }
        transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y - transformXY, transformZ);
        movingDown = true;
        isRotating = true;
    }
    [PunRPC]
    public void moveDownRPC()
    {
        if (!isRotating)
        {
            if (!MuteButton.isMute)
            {
                moveDice.Play();
            }
            float transformXY;
            if (isJumping)
            {
                transformXY = 2;
            }
            else if (isJumping3)
            {
                transformXY = 3;
            }
            else
            {
                transformXY = 1;
            }
            float transformZ;
            if (isJumping || isJumping3)
            {
                transformZ = -0.8f;
            }
            else
            {
                transformZ = 0.2f;
            }
            transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y - transformXY, transformZ);
            movingDown = true;
            isRotating = true;
        }
    }

    public void moveLeft()
    {
        if (!MuteButton.isMute)
        {
            moveDice.Play();
        }
        if (Init.isOnline && !isRotating)
        {
            photonView.RPC("moveLeftRPC", RpcTarget.All);
            return;
        }
        float transformXY;
        if (isJumping)
        {
            transformXY = 2;
        }
        else if (isJumping3)
        {
            transformXY = 3;
        }
        else
        {
            transformXY = 1;
        }
        float transformZ;
        if (isJumping || isJumping3)
        {
            transformZ = -0.8f;
        }
        else
        {
            transformZ = 0.2f;
        }
        transform.position = new Vector3(activeDice.transform.position.x - transformXY, activeDice.transform.position.y, transformZ);
        movingLeft = true;
        isRotating = true;
    }
    [PunRPC]
    public void moveLeftRPC()
    {
        if (!isRotating)
        {
            if (!MuteButton.isMute)
            {
                moveDice.Play();
            }
            float transformXY;
            if (isJumping)
            {
                transformXY = 2;
            }
            else if (isJumping3)
            {
                transformXY = 3;
            }
            else
            {
                transformXY = 1;
            }
            float transformZ;
            if (isJumping || isJumping3)
            {
                transformZ = -0.8f;
            }
            else
            {
                transformZ = 0.2f;
            }
            transform.position = new Vector3(activeDice.transform.position.x - transformXY, activeDice.transform.position.y, transformZ);
            movingLeft = true;
            isRotating = true;
        }
    }

    public void moveRight()
    {
        if (!MuteButton.isMute)
        {
            moveDice.Play();
        }
        if (Init.isOnline && !isRotating)
        {
            photonView.RPC("moveRightRPC", RpcTarget.All);
            return;
        }
        float transformXY;
        if (isJumping)
        {
            transformXY = 2;
        }
        else if (isJumping3)
        {
            transformXY = 3;
        }
        else
        {
            transformXY = 1;
        }
        float transformZ;
        if (isJumping || isJumping3)
        {
            transformZ = -0.8f;
        }
        else
        {
            transformZ = 0.2f;
        }
        transform.position = new Vector3(activeDice.transform.position.x + transformXY, activeDice.transform.position.y, transformZ);
        movingRight = true;
        isRotating = true;
    }
    [PunRPC]
    public void moveRightRPC()
    {
        if (!isRotating)
        {
            if (!MuteButton.isMute)
            {
                moveDice.Play();
            }
            float transformXY;
            if (isJumping)
            {
                transformXY = 2;
            }
            else if (isJumping3)
            {
                transformXY = 3;
            }
            else
            {
                transformXY = 1;
            }
            float transformZ;
            if (isJumping || isJumping3)
            {
                transformZ = -0.8f;
            }
            else
            {
                transformZ = 0.2f;
            }
            transform.position = new Vector3(activeDice.transform.position.x + transformXY, activeDice.transform.position.y, transformZ);
            movingRight = true;
            isRotating = true;
        }
    }

    public void StartBattle()
    {
        if (!Init.isOnline)
        {
            isBattling = true;
            battleControls.transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y, -3);
            battleControls.SetActive(true);
            Transform parentTransform = battleControls.transform;
            foreach (Transform child in parentTransform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<SpriteRenderer>().enabled = false;
                childObject.GetComponent<BattleButtonController>().ennemy = null;
                childObject.SetActive(true);
            }
        }
        if (Init.isOnline)
        {
            photonView.RPC("StartBattleRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SyncDiceRotation(bool rotating)
    {
        isRotating = rotating;
    }
    [PunRPC]
    void GlobalMecanicsRTC()
    {
        controls.SetActive(false);
        battleControls.SetActive(false);
        countEndTurn = 0;
        if (!battleMecanics.partyEnded)
        {
            main.ChangeGamerColor();
        }
    }
    [PunRPC]
    void StartBattleRPC()
    {
        isBattling = true;
        battleControls.transform.position = new Vector3(activeDice.transform.position.x, activeDice.transform.position.y, -3);
        battleControls.SetActive(true);
        Transform parentTransform = battleControls.transform;
        foreach (Transform child in parentTransform)
        {
            GameObject childObject = child.gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = false;
            childObject.GetComponent<BattleButtonController>().ennemy = null;
            childObject.SetActive(true);
        }
    }
}
