using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvents")]
public class EventData : ScriptableObject
{
    public string _eventName;
    public int gameStage;
    public Sprite sprite;
    public string _eventDescription;
}
