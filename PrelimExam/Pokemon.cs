using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon : MonoBehaviour
{

    public string name;
    public int level;
    public string element;
    public string gender;
    public string flavorText;

    public Pokemon(string name, int level, string element, string gender, string flavorText)
    {
        this.name = name;
        this.level = level;
        this.element = element;
        this.gender = gender;
        this.flavorText = flavorText;

    }
}

//public enum Element { Bug, Dark, Dragon, Electric, Fairy, Fighting, Fire, Flying, Grass, Psychic, Rock, Steel, Water, Ghost, Ground, Ice, Normal, Poison, Unknown }
//public enum Gender { Male, Female, Unknown }