using System;
using System.Collections.Generic;
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

    public int Hp {
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

    private int _initiative;

    [Header("Character characteristics")]

    // Телосложение
    [SerializeField, Range(3, 20)]
    private int _constitution;

    public int Constitution { get => _constitution; set => _constitution = value; }

    private int? _constitutionModifier = null;

    public int ConstitutionModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _constitutionModifier = CalculateModifier(_constitution);
            }
            return _constitutionModifier ??= CalculateModifier(_constitution);
        }
    }

    // Сила
    [SerializeField, Range(3, 20)]
    private int _strength;

    public int Strength { get => _strength; set => _strength = value; }

    private int? _strengthModifier = null;

    public int StrengthModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _strengthModifier = CalculateModifier(_strength);
            }
            return _strengthModifier ??= CalculateModifier(_strength);
        }
    }

    // Ловкость
    [SerializeField, Range(3, 20)]
    private int _dexterity;

    public int Dexterity { get => _dexterity; set => _dexterity = value; }

    private int? _dexterityModifier = null;

    public int DexterityModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _dexterityModifier = CalculateModifier(_dexterity);
            }
            return _dexterityModifier ??= CalculateModifier(_dexterity);
        }
    }

    // Интеллект
    [SerializeField, Range(3, 20)]
    private int _intelligence;

    public int Intelligence { get => _intelligence; set => _intelligence = value; }

    private int? _intelligenceModifier = null;

    public int IntelligenceModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _intelligenceModifier = CalculateModifier(_intelligence);
            }
            return _intelligenceModifier ??= CalculateModifier(_intelligence);
        }
    }

    // Мудрость
    [SerializeField, Range(3, 20)]
    private int _wisdom;

    public int Wisdom { get => _wisdom; set => _wisdom = value; }

    private int? _wisdomModifier = null;

    public int WisdomModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _wisdomModifier = CalculateModifier(_wisdom);
            }
            return _wisdomModifier ??= CalculateModifier(_wisdom);
        }
    }

    // Харизма
    [SerializeField, Range(3, 20)]
    private int _charisma;

    public int Charisma { get => _charisma; set => _charisma = value; }

    private int? _charismaModifier = null;

    public int CharismaModifier
    {
        get
        {
            if (Application.isEditor)
            {
                _charismaModifier = CalculateModifier(_charisma);
            }
            return _charismaModifier ??= CalculateModifier(_charisma);
        }
    }
    
    public int Protection
    {
        get
        {
            return DEFAUL_PROTECTION + _dexterity;
        }
    }

    [SerializeField]
    private List<AbstractBonus> _bonuses = new ();

    public List<AbstractBonus> Bonuses { get { return _bonuses; } }

    public void PrepareToRound()
    {
        _speed = _maxSpeed;
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

        _constitutionModifier = CalculateModifier(_constitution);
        _strengthModifier = CalculateModifier(_strength);
        _dexterityModifier = CalculateModifier(_dexterity);
        _intelligenceModifier = CalculateModifier(_intelligence);
        _wisdomModifier = CalculateModifier(_wisdom);
        _charismaModifier = CalculateModifier(_charisma);

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

    private int CalculateModifier(int attribute)
    {
        return (int)Math.Floor((float)(attribute / 2)) - 5;
    }
}
