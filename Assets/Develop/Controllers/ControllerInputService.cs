using UnityEngine;
using UnityEngine.AI;

public class ControllerManagementService : MonoBehaviour
{
    [SerializeField] GameObject _targetPointPrefab;

    [SerializeField] Character _character;

    private Controller _characterController;

    private void Awake()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.areaMask = NavMesh.AllAreas;
        queryFilter.agentTypeID = 0;

        _characterController = new CompositeCharacterController(
            new InputMousePointMovementCharacterController(
                new PointMovementCharacterController(_character, queryFilter, _targetPointPrefab)),
            new RotationCharacterControllerDependsVelocity(_character, _character));

        _characterController.Enabled();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);

        if (_character.IsDead)
            _characterController.Disabled();
    }
}
