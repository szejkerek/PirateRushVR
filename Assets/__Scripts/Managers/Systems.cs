using System;
using UnityEngine;

public class Systems : Singleton<Systems>
{
    public bool KatanaRight = false;
    public int TickRate = 32;
    public DifficultySO difficultyLevel;
}
