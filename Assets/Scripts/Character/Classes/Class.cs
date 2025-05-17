using System;
using UnityEngine;

namespace RPG.Character.Classes
{
    [Serializable]
    public class Class : MonoBehaviour
    {
        [SerializeField]
        private Dice.Types _hpDice;

        public Dice.Types HpDice { get => _hpDice; }
    }
}

