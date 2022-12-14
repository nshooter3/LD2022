using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RandomUtil : MonoBehaviour
{

    public static List<T> ChooseRandomElementsFromList<T>(List<T> list, int numElements)
    {
        if (list.Count < numElements)
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
        return list[Random.Range(0, list.Count)];
    }

    public static string ReplaceVowels(string word)
    {
        return Regex.Replace(word, "[aeiouyAEIOUY]", "_");
    }

    public static string ReplaceConsonants(string word)
    {
        return Regex.Replace(word, "[qwrtpsdfghjklzxcvbnmQWRTPSDFGHJKLZXCVBNM]", "_");
    }
}
