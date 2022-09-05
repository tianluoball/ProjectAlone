using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal")]

public class AnimalScriptableObject : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string _name;
    public string description;
    public Sprite sprite;
    public bool isAttack;
    public int moveAbility;
    public int power;
    public int HP;
    public List<GameItem> rewards;
}
