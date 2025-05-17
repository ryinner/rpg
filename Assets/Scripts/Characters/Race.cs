using System;
using System.Collections.Generic;
using RPG.Characters.Bonuses;
using UnityEngine;

namespace RPG.Characters
{
    [Serializable]
    public class Race : MonoBehaviour
    {
        [SerializeField, Range(20, 40)]
        private int _speed = 30;

        public int Speed { get => _speed; }

        [SerializeField]
        private List<AbstractBonus> _bonuses = new ();

        public List<AbstractBonus> Bonuses { get => _bonuses; }
    }
}

