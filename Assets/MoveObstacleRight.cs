using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleRight : MonoBehaviour
{ 
    public float moveSpeed = 5f; // Скорость движения
    public float movementRange = 2f; // Радиус движения (от -2 до 2)
    
    void Update()
    {
        // Вычисляем новое положение по X
        float newX = Mathf.PingPong(Time.time * moveSpeed, movementRange * 2) - movementRange;

        // Обновляем положение объекта
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
