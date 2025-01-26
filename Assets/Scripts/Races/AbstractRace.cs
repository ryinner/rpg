using UnityEngine;

public abstract class AbstractRace : MonoBehaviour
{
    private const int BASE_SPEED = 30;

    protected int _speed = BASE_SPEED;

    public int Speed { get => _speed; }

    public abstract void ApplyRaceBonuses(Character character);
}
