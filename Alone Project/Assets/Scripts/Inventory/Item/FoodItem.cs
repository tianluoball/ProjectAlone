using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "foodItem", menuName = "FoodItem")]

public class FoodItem : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string _name;
    public string description;
    public Sprite sprite;
    public Dimensions SlotDimension;
    public int staminaRecover;
    public int sansRecover;
    public string foodType;
}
