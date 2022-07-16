using UnityEngine;

public class MeleeWeapon : Weapon
{
    private float _attackGracePeriod = 0.05f;
    private float _elapsedAttackTime = 10f;
    private float _attackDelay = 0f;
    private bool _shouldAttack = false;


    private void Awake()
    {
        _isMelee = true;
        _attackDelay = 1f / _attackSpeed;
    }

    private void Update()
    {
        _elapsedAttackTime += Time.deltaTime;

        if (_shouldAttack)
            DoAttack();
    }

    public override void DoAttack()
    {
        if (_elapsedAttackTime < _attackDelay)
        {
            float remainingTime = _attackDelay - _elapsedAttackTime;
            if (remainingTime < _attackGracePeriod)
                _shouldAttack = true;

            return;
        }

        _elapsedAttackTime = 0f;
        _shouldAttack = false;
    }
}
