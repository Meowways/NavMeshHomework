using UnityEngine;

public class Health 
{
    private float _maxHealth;
    private float _currentHealth;
    private float _woundedHealthThreshold;

    private bool _isDead;

    public float CurrentHealt => _currentHealth;

    public bool _IsInjury => _currentHealth <= _woundedHealthThreshold;

    public bool IsDead => _isDead;

    public Health(float maxHealth, float woundedHealthThreshold)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;

        _woundedHealthThreshold = _maxHealth * (woundedHealthThreshold / 100);
    }

    public void TakeDamage(float damageValue)
    {
        if (damageValue < 0)
        {
            Debug.LogError("Damage value less zero!");
            return;
        }

        _currentHealth -= damageValue;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isDead = true;
        }
    }
}
