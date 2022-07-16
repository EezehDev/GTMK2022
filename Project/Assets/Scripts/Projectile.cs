using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    private List<WeaponEffect> _effects = new List<WeaponEffect>();
    private float _damage;
    private float _speed;

    private float _elapsedTime = 0f;
    private const float _maxLifetime = 10f;

    public void SetEffects(List<WeaponEffect> effects)
    {
        foreach (WeaponEffect effect in effects)
        {
            _effects.Add(effect);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _maxLifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        Health healthComp = collision.gameObject.GetComponent<Health>();
        if (healthComp)
        {
            healthComp.ChangeHealth(-_damage);
            // Apply weapon effects
        }

        // Spawn particles and hit sound effect
        Destroy(gameObject);
    }
}
