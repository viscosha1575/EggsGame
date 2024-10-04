using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubeHeight : MonoBehaviour
{  
     public float minHeight = -0.5f;  // Минимальная высота
    public float maxHeight = 0.5f;   // Максимальная высота
    public GameObject centerFloor;    // Ссылка на объект центра
    public GameObject replacementPrefab; // Префаб для замены
    public Color[] colors; // Массив доступных цветов

    void Start()
    {
        // Сохраняем список дочерних объектов
        List<Transform> children = new List<Transform>();

        // Проходим по каждому дочернему объекту (кубу) внутри префаба
        foreach (Transform child in transform)
        {
            // Проверяем, является ли текущий объект центром или рядом с ним
            if (child.gameObject == centerFloor)
            {
                // Устанавливаем высоту центра на -0.3
                child.localPosition = new Vector3(child.localPosition.x, -0.3f, child.localPosition.z);
                // Применяем случайный цвет к центру
                Color randomColor = colors[Random.Range(0, colors.Length)];
                child.GetComponent<Renderer>().material.color = randomColor; // Применяем случайный цвет
            }
            else if (IsAdjacentToCenter(child))
            {
                // Устанавливаем высоту рядом стоящих объектов на фиксированное значение (например, -0.3)
                child.localPosition = new Vector3(child.localPosition.x, -0.3f, child.localPosition.z);
                // Применяем случайный цвет к рядом стоящим объектам
                Color randomColor = colors[Random.Range(0, colors.Length)];
                child.GetComponent<Renderer>().material.color = randomColor; // Применяем случайный цвет
            }
            else
            {
                // Добавляем в список все дочерние объекты, кроме центра и соседей
                children.Add(child);

                // Генерируем случайную высоту в заданных пределах
                float randomHeight = Random.Range(minHeight, maxHeight);
                // Устанавливаем новую позицию куба с измененной высотой по Y
                child.localPosition = new Vector3(child.localPosition.x, randomHeight, child.localPosition.z);

                // Применяем случайный цвет к кубу
                Color randomColor = colors[Random.Range(0, colors.Length)];
                child.GetComponent<Renderer>().material.color = randomColor;
            }
        }

        // Генерируем случайное количество объектов для замены от 1 до 3
        int numberOfReplacements = Random.Range(1, 4);
        // Перемешиваем список дочерних объектов
        Shuffle(children);

        // Заменяем случайное количество дочерних объектов на новый префаб
        for (int i = 0; i < numberOfReplacements && i < children.Count; i++)
        {
            Transform childToReplace = children[i];
            // Заменяем объект на новый префаб
            GameObject newPrefab = Instantiate(replacementPrefab, childToReplace.position, childToReplace.rotation);
            // Применяем случайный цвет к новому префабу
            Color randomColor = colors[Random.Range(0, colors.Length)];
            newPrefab.GetComponent<Renderer>().material.color = randomColor;

            Destroy(childToReplace.gameObject); // Удаляем оригинальный объект
        }
    }

    // Метод для проверки, является ли объект соседним с центром
    private bool IsAdjacentToCenter(Transform child)
    {
        // Получаем позиции центра и текущего объекта
        Vector3 centerPosition = centerFloor.transform.localPosition;

        // Проверяем, находится ли объект в пределах 1 единицы по X и Z от центра
        return Mathf.Abs(child.localPosition.x - centerPosition.x) <= 1f &&
               Mathf.Abs(child.localPosition.z - centerPosition.z) <= 1f;
    }

    // Метод для перемешивания списка
    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Transform temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
