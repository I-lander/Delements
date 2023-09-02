using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public Sprite EarthSprite, FireSprite, WaterSprite, ThunderSprite;

    void OnCollisionEnter(Collision collision)
    {
        GameObject gameObjectCol = collision.gameObject;

        if (gameObjectCol != null && gameObjectCol.transform.parent != null)
        {
            GameObject gameObjectColParent = gameObjectCol.transform.parent.gameObject;

            if (gameObjectColParent.tag != null)
            {
                SpriteRenderer parentSprite = this.transform.parent.gameObject.transform.GetComponent<SpriteRenderer>();
                if (gameObjectColParent.tag == "Earth")
                {
                    parentSprite.sprite = EarthSprite;
                    parentSprite.enabled = true;
                }
                if (gameObjectColParent.tag == "Fire")
                {
                    parentSprite.sprite = FireSprite;
                    parentSprite.enabled = true;
                }
                if (gameObjectColParent.tag == "Water")
                {
                    parentSprite.sprite = WaterSprite;
                    parentSprite.enabled = true;
                }
                if (gameObjectColParent.tag == "Thunder")
                {
                    parentSprite.sprite = ThunderSprite;
                    parentSprite.enabled = true;
                }
            }
        }
    }
}
