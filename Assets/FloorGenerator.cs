using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
      public GameObject[] prefabs;   // Массив префабов для рандомного выбора
    public int totalPrefabs = 20; // Общее количество префабов, которое нужно заспавнить в начале
    public int respawnCount = 6;   // Количество префабов, которое нужно удалить и заспавнить при нажатии пробела
    public float spacing = 1f;      // Расстояние между префабами по оси Z

    private Queue<GameObject> spawnedPrefabs = new Queue<GameObject>(); // Очередь заспавненных префабов
    private float lastSpawnZ = -2f; // Начальная позиция спавна по оси Z

    void Start()
    {
        SpawnInitialPrefabs(); // Спавним начальные префабы
    }

    void Update()
    {
        // Проверяем нажатие пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RespawnPrefabs(); // Удаляем старые и спавним новые префабы
        }
    }

    void SpawnInitialPrefabs()
    {
        for (int i = 0; i < totalPrefabs; i++)
        {
            SpawnPrefabAtPosition(lastSpawnZ);
            lastSpawnZ += spacing; // Сдвигаем позицию на одно расстояние вперед
        }
    }

    void SpawnPrefabAtPosition(float spawnZ)
    {
        // Выбираем случайный префаб из массива
        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

        // Позиция для нового префаба
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, spawnZ);

        // Спавн выбранного префаба на позиции
        GameObject spawnedPrefab = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        spawnedPrefabs.Enqueue(spawnedPrefab); // Добавляем заспавненный префаб в очередь
    }

    void RespawnPrefabs()
    {
        // Удаляем первые 6 префабов из очереди
        for (int i = 0; i < respawnCount; i++)
        {
            if (spawnedPrefabs.Count > 0)
            {
                GameObject prefabToRemove = spawnedPrefabs.Dequeue(); // Получаем первый префаб
                Destroy(prefabToRemove); // Уничтожаем его
            }
        }

        // Спавним новые префабы на позиции, за последними заспавненными префабами
        for (int i = 0; i < respawnCount; i++)
        {
            SpawnPrefabAtPosition(lastSpawnZ);
            lastSpawnZ += spacing; // Сдвигаем позицию для следующего префаба
        }
    }
}