using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    private const int DEFAUL_PROTECTION = 10;

    [SerializeField]
    private Race _race;

    [SerializeField]
    private Class _class;

    [SerializeField, Range(1, 20)]
    private int _level;

    [SerializeField, Range(0, 100), Tooltip("If zero then it be calculated automatically"), ContextMenuItem("Calculate", nameof(CalculateMaxHp))]
    private int _maxHp;

    private int _hp;

    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            if (_hp == 0)
            {
#warning Implement die method
            }
        }
    }

    private int _maxSpeed;

    private int _speed;

    public int Speed { get => _speed; set => _speed = value; }

    private int _initiative;
    public int Initiative => _initiative;

    [Header("Character characteristics")]

    // Телосложение
    [SerializeReference]
    private Characteric _constitution = new();
    public int Constitution { get => _constitution.Value; set => _constitution.Value = value; }
    public int ConstitutionModifier { get => _constitution.Modifier; }

    // Сила
    [SerializeReference]
    private Characteric _strength = new();
    public int Strength { get => _strength.Value; set => _strength.Value = value; }
    public int StrengthModifier {get => _strength.Modifier; }

    // Ловкость
    [SerializeReference]
    private Characteric _dexterity = new();
    public int Dexterity { get => _dexterity.Value; set => _dexterity.Value = value; }
    public int DexterityModifier { get => _dexterity.Modifier; }

    // Интеллект
    [SerializeReference]
    private Characteric _intelligence = new();
    public int Intelligence { get => _intelligence.Value; set => _intelligence.Value = value; }
    public int IntelligenceModifier { get => _intelligence.Modifier; }

    // Мудрость
    [SerializeReference]
    private Characteric _wisdom  = new();
    public int Wisdom { get => _wisdom.Value; set => _wisdom.Value = value; }
    public int WisdomModifier { get => _wisdom.Modifier; }

    // Харизма
    [SerializeReference]
    private Characteric _charisma;
    public int Charisma { get => _charisma.Value; set => _charisma.Value = value; }
    public int CharismaModifier { get => _charisma.Modifier; }

    public int Protection
    {
        get
        {
            return DEFAUL_PROTECTION + DexterityModifier;
        }
    }

    [SerializeField]
    private List<AbstractBonus> _bonuses = new();

    public List<AbstractBonus> Bonuses { get { return _bonuses; } }

    public void PrepareToRound()
    {
        _speed = _maxSpeed;

        // Логика подготовки персонажа к ходу из очереди
        Debug.Log($"{_race}-{_class} ready!");
    }

    public void SetHighlight(bool isActive)
    {
        // Включение или отключение подсветки перса, сюда код подсветки с пар
        Debug.Log($"{_race}-{_class} status: {isActive}");
    }

    public int RollTheInitiative()
    {
        return Dice.Roll(Dice.Types.Twenty) + _initiative;
    }

    public void TakeDamage()
    {
#warning Implement damage action
    }

    private void Awake()
    {
        TakeBonuses(_race.Bonuses);

        _maxSpeed = _race.Speed;
#warning Remove after turn to order
        PrepareToRound();

        _level = 1;
        if (_maxHp == 0)
        {
            CalculateMaxHp();
        }

        _initiative = DexterityModifier;
    }

    private void TakeBonuses(List<AbstractBonus> bonuses)
    {
        foreach (AbstractBonus bonus in bonuses)
        {
            if (_bonuses.Contains(bonus))
            {
                continue;
            }
            _bonuses.Add(bonus);
            bonus.Apply(this);
        }
    }

    private void CalculateMaxHp()
    {
        _maxHp = 0;
        for (int i = 0; i < _level; i++)
        {
            if (_level == 1)
            {
                _maxHp += (int)_class.HpDice;
            }
            else
            {
                _maxHp += Dice.Roll(_class.HpDice);
            }
            _maxHp += ConstitutionModifier;
        }
    }
}
