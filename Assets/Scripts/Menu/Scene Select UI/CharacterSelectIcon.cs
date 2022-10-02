using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectIcon : MonoBehaviour
{
    public enum CharacterChoice { THIEF, HACKER, GAMBLER, LIZARDS, FIGHT, FINAL }

    [SerializeField] private Image icon;

    void Start()
    {
        
    }

    public void UpdateIcon(Sprite s)
    {
        icon.sprite = s;
    }
}
