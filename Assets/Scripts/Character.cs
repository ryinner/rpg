using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private const int DEFAUL_PROTECTION = 10;

    private int _maxHp;

    private int _hp;

    private int _maxSpeed;

    private int _speed;

    private int _initiative;

    // Телосложение
    private int _constitution;

    private int _constitutionAttribute;

    private int _strengthAttribute;

    // Сила
    private int _strength;

    private int _dexterityAttribute;

    // Ловкость
    private int _dexterity;

    private int _intelligenceAttribute;

    // Интеллект
    private int _intelligence;

    private int _wisdomAttribute;

    // Мудрость
    private int _wisdom;

    private int _charismaAttribute;

    // Харизма
    private int _charisma;

    private int Protection { 
        get {
            return DEFAUL_PROTECTION + _dexterity;
        } 
    }

    public void PrepareToRound()
    {
        _speed = _maxSpeed;
    }

    public int RollTheInitiative()
    {
        return Dice.Roll(Dice.Types.Twenty) + _initiative;
    }

    private void Awake()
    {
        _strength = CalculateModifier(_strengthAttribute);
        _dexterity = CalculateModifier(_dexterityAttribute);
        _intelligence = CalculateModifier(_intelligenceAttribute);
        _wisdom = CalculateModifier(_wisdomAttribute);
        _charisma = CalculateModifier(_charismaAttribute);

        _initiative = _dexterity;
    }

    private int CalculateModifier(int attribute)
    {
        return (int)Math.Floor((float)(attribute / 2)) - 5;
    }
}
