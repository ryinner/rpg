using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class IncreaseCharacterisics : AbstractBonus
{
    [SerializeField, Range(1, 2)]
    private int _point = 1;

    [SerializeField]
    private CharactericsEnum _characteric;

    public override void Apply(Character character)
    {
        switch (_characteric)
        {
            case CharactericsEnum.Strength: character.Strength += _point; break;
            case CharactericsEnum.Constitution: character.Constitution += _point; break;
            case CharactericsEnum.Charisma: character.Charisma += _point; break;
            case CharactericsEnum.Dexterity: character.Dexterity += _point; break;
            case CharactericsEnum.Intelligence: character.Intelligence += _point; break;
            case CharactericsEnum.Wisdom: character.Wisdom += _point; break;
            default: throw new ArgumentException("Unknown characteristics type" + _characteric.HumanName());
        }
    }
}