using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{

    void Update()
    {
    }

    public bool ManageControlsEnterCollision(Collider2D other)
    {
        if (other == null)
        {
            return false;
        }
        if (other.tag == "Wall" || other.tag == "Dice")
        {
            return true;
        }
        return false;
    }

    public bool ManageControlsExitCollision(Collider2D other)
    {
        if (other.tag == "Wall" || other.tag == "Dice")
        {
            return false;
        }
        return true;
    }


}
