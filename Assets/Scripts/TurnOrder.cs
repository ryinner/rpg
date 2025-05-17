using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Characters;
using UnityEngine;

// Это про логику хождения персонажами
/* void Start()
{
    var turnOrder = new TurnOrder();
    turnOrder.Initialize(new List<Character>
    {
        new Character { Name = "HumanWarrior", Initiative = 15 },
        new Character { Name = "HumanMag", Initiative = 20 },
        new Character { Name = "ElfWarrior", Initiative = 18 }
    });

    turnOrder.NextTurn(); // Ход человека-мага
    turnOrder.NextTurn(); // Ход человека-война
    turnOrder.NextTurn(); // Ход эльфа-война
} */

public class TurnOrder : MonoBehaviour
{
    public List<Character> Characters { get; private set; } = new List<Character>();
    private int currentTurnIndex = 0;

    // Иницаилизация и получаем листик с персонажами
    public void Initialize(List<Character> characters)
    {
        Characters = characters;
        RollInitiative();
        currentTurnIndex = 0; // Сбрасываем индекс при инициализации на всякий
        UpdateTurnOrderUI();
    }

    private void RollInitiative()
    {
        foreach (var character in Characters)
        {
            character.PrepareToFigth();
        }
        Characters = Characters.OrderByDescending(c => c.CurrentInitiative).ToList();
    }

    // Переключение персоанажа на следующего
    public void NextTurn()
    {
        if (Characters.Count == 0)
        {
            Debug.LogWarning("Персонажи в очереди отстуствуют!");
            return;
        }

        var currentCharacter = Characters[currentTurnIndex]; // Текущий персонаж

        HighlightActiveCharacter(currentCharacter); // Подсветка и активация персо
        currentCharacter.PrepareToRound();

        currentTurnIndex = (currentTurnIndex + 1) % Characters.Count; // Переход к след.персонажу
    }

    // Подсветка активного-текущего персонажа
    private void HighlightActiveCharacter(Character character)
    {
        foreach (var c in Characters)
        {
            c.SetHighlight(false);
        }

        character.SetHighlight(true);
    }

    private void UpdateTurnOrderUI()
    {
        // Дописать будующее обновление с UI
    }
}