using System;
using UnityEngine;

namespace RPG.Character.Bonuses
{
    [Serializable]
    public abstract class AbstractBonus : MonoBehaviour
    {
        public abstract void Apply(Character character);
    }
}
