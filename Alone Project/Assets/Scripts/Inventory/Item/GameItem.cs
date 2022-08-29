using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameItem", menuName = "GameItem")]

public class GameItem : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string _name;
    public string description;
    public Sprite sprite;
    public Dimensions SlotDimension;
    public int itemCost;
    public int power;
    public string resourceType;
}

[Serializable]
public struct Dimensions
{
    public int Height;
    public int Width;
}
