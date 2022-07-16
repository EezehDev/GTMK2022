using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth = 1f;
    private bool _isDead = false;

    public Action<bool> Died;
    public Action<float> HealingReceived;
    public Action<float> DamageTaken;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ChangeHealth(float delta)
    {
        if (_isDead) return;

        _currentHealth += delta;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        if (delta > 0)
        {
            HealingReceived.Invoke(delta);
        }
        else if (delta < 0)
        {
            DamageTaken.Invoke(-delta);
        }

        if (_currentHealth < 0.1f)
        {
            _isDead = true;
            Died.Invoke(true);
        }
    }
}
