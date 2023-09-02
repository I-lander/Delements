using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonController : MonoBehaviourPun
{
    DicePivotScript dps;
    Main main;
    BattleMecanics battleMecanics;
    public Sprite EarthSprite, FireSprite, WaterSprite, ThunderSprite;

    public ControlsScript controlsScript;

    public bool isColliding = false;

    private void Awake()
    {
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;
        main = GameObject.FindObjectOfType(typeof(Main)) as Main;
    }
    void Update()
    {
        if (this.tag == "Jump3Button" && dps.activeDice.faceUp != "Thunder")
        {
            gameObject.SetActive(false);
            return;
        }
        if (this.tag == "JumpButton" || this.tag == "Jump3Button")
        {
            BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
            Vector2 point = boxCollider.bounds.center;
            Vector2 size = boxCollider.bounds.size;
            boxCollider.enabled = false;
            Collider2D collision = Physics2D.OverlapBox(point, size, 0f);
            boxCollider.enabled = true;
            isColliding = controlsScript.ManageControlsEnterCollision(collision);
            if (!isColliding)
            {
                gameObject.SetActive(true);
                SetJumpButtonSprite();
            }
        }
        if (isColliding || dps.isRotating)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (Init.isOnline)
        {
            main = GameObject.FindObjectOfType(typeof(Main)) as Main;
            getOwnership();
        }
    }

    void OnDisable()
    {
        if (this.tag == "JumpButton")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isColliding = controlsScript.ManageControlsEnterCollision(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isColliding = controlsScript.ManageControlsExitCollision(other);
    }

    void OnMouseDown()
    {
        if (main.isPaused || (Init.isOnline && !main.isLocalPlayer()) || (Init.isOnline && !photonView.IsMine))
        {
            return;
        }
        if (!dps.isRotating && !isColliding)
        {
            if (this.gameObject.tag == "JumpButton")
            {
                if (!Init.isOnline)
                {
                    dps.isJumping = true;
                }

                if (Init.isOnline)
                {
                    photonView.RPC("updateJumpRPC", RpcTarget.All);
                }
            }
            if (this.gameObject.tag == "Jump3Button")
            {
                if (!Init.isOnline)
                {
                    dps.isJumping3 = true;
                }

                if (Init.isOnline)
                {
                    photonView.RPC("updateJump3RPC", RpcTarget.All);
                }
            }
            if (this.gameObject.name == "buttonUp" || this.gameObject.name == "buttonJumpUp" || this.gameObject.name == "buttonJump3Up")
            {
                dps.moveUp();
            }
            if (this.gameObject.name == "buttonDown" || this.gameObject.name == "buttonJumpDown" || this.gameObject.name == "buttonJump3Down")
            {
                dps.moveDown();
            }
            if (this.gameObject.name == "buttonLeft" || this.gameObject.name == "buttonJumpLeft" || this.gameObject.name == "buttonJump3Left")
            {
                dps.moveLeft();
            }
            if (this.gameObject.name == "buttonRight" || this.gameObject.name == "buttonJumpRight" || this.gameObject.name == "buttonJump3Right")
            {
                dps.moveRight();
            }
            if (Init.isOnline)
            {
                photonView.RPC("updateIsBattledRPC", RpcTarget.All);
                return;
            }
            dps.isBattled = false;
        }
    }

    void SetJumpButtonSprite()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        if (dps.activeDice.faceUp == "Earth")
        {
            sprite.sprite = EarthSprite;
            sprite.enabled = true;
        }
        if (dps.activeDice.faceUp == "Fire")
        {
            sprite.sprite = FireSprite;
            sprite.enabled = true;
        }
        if (dps.activeDice.faceUp == "Water")
        {
            sprite.sprite = WaterSprite;
            sprite.enabled = true;
        }
        if (dps.activeDice.faceUp == "Thunder")
        {
            sprite.sprite = ThunderSprite;
            sprite.enabled = true;
        }
    }

    [PunRPC]
    public void updateIsBattledRPC()
    {
        dps.isBattled = false;
    }
    [PunRPC]
    public void updateJumpRPC()
    {
        dps.isJumping = true;
    }
    [PunRPC]
    public void updateJump3RPC()
    {
        dps.isJumping3 = true;
    }

    void getOwnership()
    {
        if (main.isLocalPlayer() && !photonView.IsMine)
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
