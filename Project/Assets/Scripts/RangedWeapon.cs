using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Spawning")]
    [SerializeField] private Transform _fireSocket = null;
    [SerializeField] private GameObject _projectilePrefab = null;

    [Header("Stats")]
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _speed = 1f;

    public override void DoAttack()
    {
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
                projComp.SetSpeed(_speed);
            }
        }
    }
}
