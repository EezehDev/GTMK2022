using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected bool _isMelee = false;
    [SerializeField] protected List<WeaponEffect> _effects = new List<WeaponEffect>();

    public abstract void DoAttack();

    public bool IsMelee()
    {
        return _isMelee;
    }
}
