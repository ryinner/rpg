using System;
using UnityEngine;

[RequireComponent(typeof(AbstractRace))]
public class Character : MonoBehaviour
{
    private const int DEFAUL_PROTECTION = 10;

    [SerializeField]
    private AbstractRace _race;

    [SerializeField]
    private AbstractClass _class;

    [SerializeField, Range(1, 20)]
    private int _level;

    [SerializeField, Range(6, 100)]
    private int _maxHp;

    private int _hp;

    private int _maxSpeed;

    private int _speed;

    private int _initiative;

    // Телосложение
    [SerializeField, Range(3, 20)]
    private int _constitution;

    private int _constitutionModifier;

    // Сила
    [SerializeField, Range(3, 20)]
    private int _strength;

    private int _strengthModifier;

    // Ловкость
    [SerializeField, Range(3, 20)]
    private int _dexterity;

    private int _dexterityModifier;

    // Интеллект
    [SerializeField, Range(3, 20)]
    private int _intelligence;

    private int _intelligenceModifier;

    // Мудрость
    [SerializeField, Range(3, 20)]
    private int _wisdom;

    private int _wisdomModifier;

    // Харизма
    [SerializeField, Range(3, 20)]
    private int _charisma;

    private int _charismaModifier;

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
        _race = GetComponent<AbstractRace>();

        _race.ApplyRaceBonuses(this);
        _maxSpeed = _race.Speed;

        _constitutionModifier = CalculateModifier(_constitution);
        _strengthModifier = CalculateModifier(_strength);
        _dexterityModifier = CalculateModifier(_dexterity);
        _intelligenceModifier = CalculateModifier(_intelligence);
        _wisdomModifier = CalculateModifier(_wisdom);
        _charismaModifier = CalculateModifier(_charisma);

        _level = 1;
        if (_maxHp == 0)
        {
            _maxHp = CalculateMaxHp();
        }
        _hp = _maxHp;
        _initiative = _dexterity;
    }

    private int CalculateMaxHp()
    {
        int hp = 0;
        for (int i = 0; i < _level; i++)
        {
            if (_level == 1)
            {
                hp += (int)_class.HpDice;
            }
            else
            {
                hp += Dice.Roll(_class.HpDice);
            }
            hp += _constitutionModifier;
        }
        return hp;
    }

    private int CalculateModifier(int attribute)
    {
        return (int)Math.Floor((float)(attribute / 2)) - 5;
    }
}
