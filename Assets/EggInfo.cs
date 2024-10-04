using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggInfo : MonoBehaviour
{
    public int activeChildIndex; // Хранит индекс активного ребенка

    // Этот метод вызывается из SwitchEggs, когда активируется ребенок
    public void SetActiveChildIndex(int index)
    {
        activeChildIndex = index;
    }
}
