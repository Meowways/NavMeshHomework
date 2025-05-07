using UnityEngine;

public class DamageExplosionBomb : MonoBehaviour
{
    [SerializeField] private float _damageValue;
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _timeBeforeExplosion;

    [SerializeField] private ParticleSystem _explosion;

    private IExplosionEffect _explosionEffect;

    private bool _isActivated;

    private void Awake()
    {
        _isActivated = false;

        _explosionEffect = new DamagingExplosion(_damageValue, _radiusExplosion, _explosion);
    }

    private void Update()
    {
        if (_isActivated == false)
            return;


        _timeBeforeExplosion -= Time.deltaTime;

        if (_timeBeforeExplosion <= 0)
        {
            _explosionEffect.Execute(transform.position);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable characterHealth = other.GetComponent<IDamageable>();

        if (characterHealth != null)
            _isActivated = true; 
    }

    private void OnDrawGizmos()
    {
        if (_isActivated == false) 
            return;

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, _radiusExplosion);
    }
}
