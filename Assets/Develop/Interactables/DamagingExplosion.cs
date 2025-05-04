using UnityEngine;

public class DamagingExplosion : IExplosionEffect
{
    private float _damageValue;
    private float _radiusExplosion;

    private ParticleSystem _explosion;

    public DamagingExplosion(float damageValue, float radiusExplosion, ParticleSystem explosion)
    {
        _damageValue = damageValue;
        _radiusExplosion = radiusExplosion;

        _explosion = explosion;
    }

    public void Execute(Vector3 original)
    {
        GameObject.Instantiate(_explosion, original, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(original, _radiusExplosion);

        if (colliders.Length > 0)
            foreach (Collider collider in colliders)
            {
                IDamageable characterHealth = collider.GetComponent<IDamageable>();

                if (characterHealth != null)
                    characterHealth.TakeDamage(_damageValue);
            }
    }
}
