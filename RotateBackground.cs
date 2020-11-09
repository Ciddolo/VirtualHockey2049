using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    public float Angle;
    void Update()
    {
        transform.Rotate(Vector3.up, Angle * Time.deltaTime);
    }
}
