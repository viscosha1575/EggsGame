using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения

    void Update()
    {
        // Вращение объекта вокруг оси Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
