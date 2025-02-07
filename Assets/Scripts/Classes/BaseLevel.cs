using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseLevel
{
    [SerializeField, Range(1, 1)]
    private int _level;

    public int Level { get => _level; }

    [SerializeField]
    private List<AbstractBonus> _bonuses = new ();

    public List<AbstractBonus> Bonuses { get => _bonuses; }
}
