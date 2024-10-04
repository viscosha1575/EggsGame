using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{   public GameObject prefabToSpawn; // Основной префаб, который будем спавнить
    public GameObject prefabEggs; // Префаб для яиц
    public GameObject[] randomPrefabsObstacle; // Массив из трех префабов для спавна препятствий
    private Vector3 spawnPosition = new Vector3(0, 0, 12); // Начальная позиция для следующего спавна платформ
    private Vector3 spawnEggPosition = new Vector3(0, 0, 2f); // Начальная позиция для яиц
    private float zOffset = 6f; // Смещение по оси Z для спавна платформ и яиц
    private float obstacleZPosition = 2.8f; // Начальная позиция для препятствий
    private bool isEggDestroyed = true; // Флаг, указывающий, что яйцо уничтожено

    void Start()
    {
        // Спавним два объекта платформы изначально
        SpawnWithRandomPrefab(new Vector3(0, 0, 0));
        SpawnWithRandomPrefab(new Vector3(0, 0, 6));

        // Спавним первое яйцо на позиции Z = 5
        SpawnEggs(spawnEggPosition);
        SpawnObstacle(); // Спавним первое препятствие
    }

    void Update()
    {
        // Проверяем нажатие клавиши пробела и если предыдущее яйцо уничтожено
        if (Input.GetKeyDown(KeyCode.Space) && isEggDestroyed)
        {
            // Спавним платформу и случайный объект сверху
            SpawnWithRandomPrefab(spawnPosition);
            
            // Увеличиваем координаты для следующего спавна платформы и яйца
            spawnPosition.z += zOffset;
            spawnEggPosition.z += zOffset;

            // Спавним яйцо на новой позиции
            SpawnEggs(spawnEggPosition);

            // Спавним препятствие на новой позиции
            SpawnObstacle();

            // Сбрасываем флаг до уничтожения нового яйца
            isEggDestroyed = false;
        }
    }

    // Метод для спавна платформы и случайного объекта над ней
    void SpawnWithRandomPrefab(Vector3 position)
    {
        // Спавним платформу
        Instantiate(prefabToSpawn, position, Quaternion.identity);
    }

    // Метод для спавна яиц
    void SpawnEggs(Vector3 position)
    {
        Vector3 eggPosition = position + new Vector3(0, 1, 0); // Добавьте высоту
        Instantiate(prefabEggs, eggPosition, Quaternion.identity);
    }

    // Метод для спавна препятствий на новой позиции
    void SpawnObstacle()
    {
        // Случайно выбираем один из трёх префабов препятствий
        int randomIndex = Random.Range(0, randomPrefabsObstacle.Length);

        // Спавним выбранный объект на текущей позиции Z
        Vector3 obstaclePosition = new Vector3(0, 1, obstacleZPosition); // Фиксированная позиция по Y и X

        // Спавним препятствие
        Instantiate(randomPrefabsObstacle[randomIndex], obstaclePosition, Quaternion.identity);

        // Увеличиваем позицию Z для следующего препятствия
        obstacleZPosition += 6f; // Увеличиваем Z на 10 для следующего спавна (5, 15, 25 и т.д.)
    }

    // Метод вызывается, когда яйцо уничтожено
    public void EggDestroyed()
    {
        isEggDestroyed = true; // Яйцо уничтожено, можно спавнить новое
    }
}
