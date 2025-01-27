using System;
using UnityEngine;

[Serializable]
public abstract class AbstractBonus : MonoBehaviour
{
    public abstract void Apply(Character character);
}