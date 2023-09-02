using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFaceDetection : MonoBehaviour
{
    public string faceUp;

    void OnCollisionEnter(Collision collision)
    {
        var faceUpObj = collision.contacts[0].otherCollider.transform.gameObject.transform.parent;
        faceUp = faceUpObj.tag;

        var faceUpParent = faceUpObj.gameObject.transform.parent;
        if (faceUpParent != null)
        {
            faceUpParent.GetComponent<ActiveDice>().faceUp = faceUp;
        }
    }

}
