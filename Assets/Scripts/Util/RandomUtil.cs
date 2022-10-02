using System.Collections.Generic;
using UnityEngine;

public class RandomUtil : MonoBehaviour
{

    public static List<T> ChooseRandomElementsFromList<T>(List<T> list, int numElements)
    {
        if (list.Count <= numElements)
        {
            return list;
        }
        List<T> remainingList = new List<T>(list);
        List<T> pickedList = new List<T>();
        for (int i = 0; i < numElements; i++)
        {
            T pickedItem = GetRandomElementFromList(remainingList);
            pickedList.Add(pickedItem);
            remainingList.Remove(pickedItem);
        }
        return pickedList;
    }

    public static T GetRandomElementFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count - 1)];
    }
}
