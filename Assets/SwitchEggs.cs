using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEggs : MonoBehaviour
{
     public GameObject[] children; // Массив для хранения детей (объектов)
    private EggInfo eggInfo; // Ссылка на компонент EggInfo

    void Start()
    {
        eggInfo = GetComponent<EggInfo>(); // Получаем ссылку на компонент EggInfo
        StartCoroutine(SwitchChildren());
    }

    private IEnumerator SwitchChildren()
    {
        // Отключаем всех детей для начала
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }

        // Включаем первого ребенка и обновляем информацию в EggInfo
        children[0].SetActive(true);
        eggInfo.SetActiveChildIndex(0); // Устанавливаем индекс активного ребенка
        yield return new WaitForSeconds(1); // Ждем 1 секунду

        // Включаем второго ребенка и обновляем информацию
        children[0].SetActive(false);
        children[1].SetActive(true);
        eggInfo.SetActiveChildIndex(1);
        yield return new WaitForSeconds(1); // Ждем 1 секунду

        // Включаем третьего ребенка и обновляем информацию
        children[1].SetActive(false);
        children[2].SetActive(true);
        eggInfo.SetActiveChildIndex(2);
    }
}
