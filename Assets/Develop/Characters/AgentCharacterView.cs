using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int RunningKey = Animator.StringToHash("IsRunning");
    private readonly int DeadKey = Animator.StringToHash("IsDie");

    private const int MinWeight = 0;
    private const int MaxWeight = 1;

    private const string InjurLayer = "Injur Layer";

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private void Update()
    {
        if (_character.IsDead)
            return;

        if (_character.IsCharacterMoving())
            RunningEnable();
        else
            RunningDisable();

        if (_character.IsDead)
            _animator.SetTrigger(DeadKey);

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

    private void RunningEnable() => _animator.SetBool(RunningKey, true);

    private void RunningDisable() => _animator.SetBool(RunningKey, false);

}
