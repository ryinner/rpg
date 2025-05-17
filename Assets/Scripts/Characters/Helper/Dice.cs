using UnityEngine;

public class Dice
{
    public enum Types {
        Four = 4,
        Six = 6,
        Eight = 8,
        Ten = 10,
        Twelve = 12,
        Twenty = 20
    }

    public enum Mode {
        Default,
        Advantage,
        Disadvantage,
    }

    public static int Roll(Types dice, Mode mode = Mode.Default)
    {
        if (mode == Mode.Default)
        {
            return Random.Range(0, (int)dice);
        }

        int firstRoll = Random.Range(0, (int)dice);
        int secondRoll = Random.Range(0, (int)dice);

        if (mode == Mode.Advantage)
        {
            return Mathf.Max(firstRoll, secondRoll);
        }
        return Mathf.Min(firstRoll, secondRoll);
    }
}
