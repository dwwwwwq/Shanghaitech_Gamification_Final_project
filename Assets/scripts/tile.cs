using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 position;
    public bool isRotatable;
    public float rotationAngle;

    

   

    public void RotateTile(float angle)
    {
        if (isRotatable)
        {
            transform.Rotate(Vector3.up, angle);
            rotationAngle += angle;
        }
    }
}