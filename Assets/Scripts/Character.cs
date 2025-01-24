using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int _maxHp;

    private int _hp;

    private int _maxSpeed;

    private int _speed;

    private int Protection { get; set; }

    public void PrepareToNewRound()
    {
        _speed = _maxSpeed;
    }
}
