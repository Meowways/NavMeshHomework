using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int RunningKey = Animator.StringToHash("IsRunning");
    private readonly int RelaxKey = Animator.StringToHash("IsRelax");
    private readonly int DeadKey = Animator.StringToHash("IsDie");

    private const int MinWeight = 0;
    private const int MaxWeight = 1;

    private const float MinVelocityForMovement = 0.05f;

    private const string InjurLayer = "Injur Layer";

    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    [SerializeField] private float _restTime;

    private float _restTimer;

    private bool _isDead;

    private void Awake()
    {
        _isDead = false;

        ResetTimer();
    }

    private void Update()
    {
        if (_isDead)
            return;

        if (IsCharacterMoving())
        {
            RunningEnable();

            ResetTimer();
        }
        else
        {
            RunningDisable();

            _restTimer -= Time.deltaTime;

            if (_restTimer <= 0)
                _animator.SetTrigger(RelaxKey);
        }

        if (_character.IsDead)
        {
            _animator.SetTrigger(DeadKey);
            _isDead = true;
        }

        if (_character.IsInjur)
            SetWeightLayerTo(InjurLayer, MaxWeight);
    }

    public void SetWeightLayerTo(string layer, int weight)
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex(layer), weight);
    }
    public void DisableHitLayer()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("HitLayer"), MinWeight);
    }

    public void ResetTimer()
    {
        _restTimer = _restTime;
    }

    private bool IsCharacterMoving() => _character.CurrentVelocity.magnitude >= MinVelocityForMovement;


    private void RunningEnable() => _animator.SetBool(RunningKey, true);

    private void RunningDisable() => _animator.SetBool(RunningKey, false);

}
