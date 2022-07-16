using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Projectile")]
    [SerializeField] private Transform _fireSocket = null;
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField] private float _projectileSpeed = 1f;

    private float _attackGracePeriod = 0.05f;
    private float _elapsedAttackTime = 10f;
    private float _attackDelay = 0f;
    private bool _shouldAttack = false;

    private void Awake()
    {
        _isMelee = false;
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

        if (_projectilePrefab == null) return;

        GameObject projectile = null;
        if (_fireSocket == null)
        {
            projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        }
        else
        {
            projectile = Instantiate(_projectilePrefab, _fireSocket.position, _fireSocket.rotation);
        }

        if (projectile)
        {
            Projectile projComp = projectile.GetComponent<Projectile>();
            if (projComp)
            {
                projComp.SetEffects(_effects);
                projComp.SetDamage(_damage);
                projComp.SetSpeed(_projectileSpeed);
            }
        }

        _elapsedAttackTime = 0f;
        _shouldAttack = false;
    }
}
