using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMainScreen : MonoBehaviour
{
    public float rotationSpeed = 30f;
    void Update()
    {
        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.Euler(0f, Time.deltaTime * rotationSpeed, 0f);

        transform.rotation = currentRotation * targetRotation;
    }
}
