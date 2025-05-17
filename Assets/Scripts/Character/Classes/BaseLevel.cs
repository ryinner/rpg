using System;
using System.Collections.Generic;
using RPG.Character.Bonuses;
using UnityEngine;

namespace RPG.Character.Classes
{
    [Serializable]
    public class BaseLevel
    {
        [SerializeField, Range(1, 1)]
        private int _level;

        public int Level { get => _level; }

        [SerializeField]
        private List<AbstractBonus> _bonuses = new ();

        public List<AbstractBonus> Bonuses { get => _bonuses; }
    }
}

