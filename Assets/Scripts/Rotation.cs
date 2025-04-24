using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 10f; 

    void Update()
    {
        transform.Rotate(rotationSpeed, 0f, 0f * Time.deltaTime);
    }

}
