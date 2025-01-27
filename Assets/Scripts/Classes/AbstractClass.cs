using UnityEngine;

public abstract class AbstractClass : MonoBehaviour
{
    public Dice.Types HpDice { get => GetHpDice(); }

    protected abstract Dice.Types GetHpDice();
}
