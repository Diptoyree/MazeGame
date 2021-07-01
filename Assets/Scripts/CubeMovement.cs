using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float Second;
    public Vector3 Move;
    public Vector3 Rotate;
    float TempTime = 0;
    public bool Movement;
    public bool Rotation;

    void Update()
    {
        TempTime += Time.deltaTime;
        if (TempTime >= Second)
        {
            TempTime = 0;
            Move *= -1;
            Rotate *= -1;
        }
        if (Movement)
        {
            transform.Translate(Move * Time.deltaTime);
        }
        if (Rotation)
        {
            transform.Rotate(Rotate);
        }
    }
}
