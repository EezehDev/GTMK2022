using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Multipliers
    private float _movementMultiplier = 1f;
    private float _damageMultiplier = 1f;
    private float _attackSpeed = 1f;

    private float _damageOverTime = 0f;

    private float _movementSlowRemaining = 0f;
    private float _movementGainRemaining = 0f;
    private float _rampageRemaining = 0f;
    private float _attackSpeedRemaining = 0f;
    private float _dotRemaining = 0f;

    private const float _slowTime = 3f;
    private const float _speedTime = 3f;
    private const float _damageTime = 3f;
    private const float _asTime = 3f;
    private const float _dotTime = 3f;

    public void SlowMovementSpeed()
    {
        _movementSlowRemaining = _slowTime;
    }

    public void IncreaseMovementSpeed()
    {
        _movementGainRemaining = _speedTime;
    }

    public void IncreaseDamage()
    {
        _rampageRemaining = _damageTime;
    }

    public void IncreaseAttackSpeed()
    {
        _attackSpeedRemaining = _asTime;
    }

    public void ActivateDOT()
    {
        _dotRemaining = _dotTime;
    }
}
