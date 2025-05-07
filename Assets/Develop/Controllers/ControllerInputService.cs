using UnityEngine;

public class ControllerManagementService : MonoBehaviour
{
    [SerializeField] AgentCharacter _agentCharacter;

    [SerializeField] GameObject _targetPointPrefab;
    [SerializeField] LayerMask _layerMask;

    private Controller _mouseInputController;
    private Controller _randomPointController;

    private void Awake()
    {
        _randomPointController = new RandomMovementController(_agentCharacter);

        _mouseInputController = new CompositeController(new InputMousePointMovementController(
            new PointMovementAgentController(_agentCharacter, _targetPointPrefab, _layerMask), _agentCharacter),
            new RotationCharacterControllerDependsVelocity(_agentCharacter, _agentCharacter));

        _mouseInputController.Enabled();
    }

    private void Update()
    {
        _mouseInputController.Update(Time.deltaTime);

        if (_agentCharacter.IsDead)
        {
            _mouseInputController.Disabled();
            _randomPointController.Disabled();
        }

        if (_agentCharacter.IsBored)
        {
            _randomPointController.Update(Time.deltaTime);
            _randomPointController.Enabled();
        }

        if (_agentCharacter.IsBored == false)
            _randomPointController.Disabled();
    }
}
