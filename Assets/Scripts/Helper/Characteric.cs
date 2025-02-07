using System;
using UnityEngine;

public class Characteric
{
    // Original value
    [SerializeField, Range(3, 20), ContextMenuItem("Generate random value", nameof(Random))]
    private int _value;

    public int Value { get => _value; set => _value = value; }

    private bool _isMaster = false; 

    public bool IsMaster { get => _isMaster; set => _isMaster = value; }

    // Add to roll
    private int? _modifier = null;

    public int Modifier {
        get
        {
            if (Application.isEditor)
            {
                _modifier = CalculateModifier(_value);
            }
            return _modifier??= CalculateModifier(_value);
        }
    }

    public int RollSavingThrow(Dice.Mode mode, int bonus)
    {
        return Dice.Roll(Dice.Types.Twenty, mode) + Modifier + (IsMaster ? bonus : 0);
    }

    private int CalculateModifier(int attribute)
    {
        return (int)Math.Floor((float)(attribute / 2)) - 5;
    }

    private void Random()
    {
        _value = UnityEngine.Random.Range(3, 20);
    }
}