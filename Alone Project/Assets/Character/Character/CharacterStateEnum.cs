using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateEnum : MonoBehaviour
{
    public enum PawnState
    {
        Idle = 0,
        Moving = 1
    }

    public enum PawnFacingVertical
    {
        Up = 0,
        Down = 1
    }

    public enum PawnFacingHorizontal
    {
        Right = 0,
        Left = 1
    }
}
