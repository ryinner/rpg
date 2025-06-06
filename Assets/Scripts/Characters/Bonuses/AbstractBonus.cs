using System;
using UnityEngine;

namespace RPG.Characters.Bonuses
{
    [Serializable]
    public abstract class AbstractBonus : MonoBehaviour
    {
        public abstract void Apply(Character character);

        public abstract void Cancel(Character character);
    }
}
