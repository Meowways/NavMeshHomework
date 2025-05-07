using UnityEngine;
using UnityEngine.AI;

public class PointMovementAgentController : PointMovementController
{
    private AgentCharacter _character;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public PointMovementAgentController(AgentCharacter character, GameObject targetPointPrefab, LayerMask layerMask) : base(targetPointPrefab, layerMask)
    {
        _character = character;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (IsTargetPointSet() && _character.TryGetPath(_targetPoint, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReache(distanceToTarget) == false)
            {
                _character.ResumeMove();
                _character.SetDestination(_targetPoint);
                return;
            }
        }

        _character.StopMove();

        ClearTargetPoint();
    }

    public override void Disabled()
    {
        base.Disabled();

        _character.StopMove();
    }
}
