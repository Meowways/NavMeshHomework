using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMover, IDirectionalRotator, IDamageable
{
    private const float MinVelocityForMovement = 0.05f;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _injuredHealthPercentage;

    [SerializeField] private Animator _animator; 

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;
    private Health _health;

    public Vector3 Position => transform.position;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public float CurrentHealth => _health.CurrentHealt;

    public bool IsInjur => _health._IsInjury;

    public bool IsDead => _health.IsDead;


    private void Awake()
    {
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotateSpeed);

        _health = new Health(_maxHealth, _injuredHealthPercentage);
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public bool IsCharacterMoving() => CurrentVelocity.magnitude >= MinVelocityForMovement;

    public void SetMoveDirection(Vector3 inputDirection)
    {
        _mover.SetInputDirection(inputDirection);
    }

    public void SetRotateDirection(Vector3 inputDirection)
    {
        _rotator.SetInputDirection(inputDirection);
    }

    public void TakeDamage(float damageValue)
    {
        SetHitLayerWeight(1);
        _health.TakeDamage(damageValue);
    }

    public void SetHitLayerWeight(int weight)
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("HitLayer"), weight);
        _animator.SetTrigger("IsHit");
    }
}
