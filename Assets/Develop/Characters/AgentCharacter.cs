using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalRotator, IDamageable
{
    private const float MinVelocityForMovement = 0.05f;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _injuredHealthPercentage;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeToBored;

    private float _timerToBored;

    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private Health _health;

    private bool _isBored;

    public Vector3 Position => transform.position;

    public Vector3 CurrentVelocity => _agent.desiredVelocity;

    public float CurrentHealth => _health.CurrentHealt;

    public bool IsInjur => _health._IsInjury;

    public bool IsDead => _health.IsDead;

    public bool IsBored => _isBored;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotateSpeed);

        _health = new Health(_maxHealth, _injuredHealthPercentage);

        SetNotBored();
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);

        if (IsCharacterMoving() == false)
        {
            _timerToBored -= Time.deltaTime;

            if (_timerToBored <= 0)
                SetBored();
        }
    }

    public bool IsCharacterMoving() => CurrentVelocity.magnitude >= MinVelocityForMovement;

    public void SetDestination(Vector3 destination) => _mover.SetDestination(destination);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetBored() => _isBored = true;

    public void SetNotBored()
    {
        _isBored = false;
        _timerToBored = _timeToBored;
    }

    public void SetRotateDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

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
