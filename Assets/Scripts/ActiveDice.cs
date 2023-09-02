using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDice : MonoBehaviour
{
    DicePivotScript dps;

    public Sprite ThunderSprite;

    public string faceUp;

    void Update()
    {
        dps = GameObject.FindObjectOfType(typeof(DicePivotScript)) as DicePivotScript;

        if (dps == null || dps.activeDice == null)
        {
            return;
        }
        if (dps.activeDice && dps.activeDice.transform.position.y >= 6 && dps.activeDice.tag == "BlackDice")
        {
            TransformDice();
        }
        if (dps.activeDice && dps.activeDice.transform.position.y <= -6 && dps.activeDice.tag == "WhiteDice")
        {
            TransformDice();
        }
    }

    void TransformDice()
    {
        Transform parentTransform = dps.activeDice.transform;
        foreach (Transform child in parentTransform)
        {
            GameObject childObject = child.gameObject;
            if (childObject.GetComponent<SpriteRenderer>())
            {
                SpriteRenderer Childsprite = childObject.GetComponent<SpriteRenderer>();
                Childsprite.sprite = ThunderSprite;
                childObject.tag = "Thunder";
            }
        }
        dps.activeDice.GetComponent<ActiveDice>().faceUp = "Thunder";
    }
}
