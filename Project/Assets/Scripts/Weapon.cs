using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] protected bool _isMelee = false;
    [SerializeField] protected List<WeaponEffect> _effects = new List<WeaponEffect>();

    [Header("Stats")]
    [SerializeField] protected float _attackSpeed = 1f; // attacks per second
    [SerializeField] protected float _damage = 1f;

    public abstract void DoAttack();

    public bool IsMelee()
    {
        return _isMelee;
    }
}
