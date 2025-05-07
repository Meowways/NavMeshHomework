using UnityEngine;
using UnityEngine.AI;

public class PointMovementCharacterController : PointMovementController
{
    private const int MinCornersCountInPathToMove = 2;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private IDirectionalMover _mover;

    private NavMeshPath _pathToTarget = new NavMeshPath();
    private NavMeshQueryFilter _queryFilter;

    public PointMovementCharacterController(IDirectionalMover mover, NavMeshQueryFilter queryFilter, GameObject targetPointPrefab, LayerMask layerMask) : base (targetPointPrefab, layerMask)
    {
        _mover = mover;

        _queryFilter = queryFilter;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (IsTargetPointSet() && NavMeshUtils.TryGetPath(_mover.Position, _targetPoint, _queryFilter, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReache(distanceToTarget) == false && EnoughCornersInPath(_pathToTarget))
            {
                _mover.SetMoveDirection(_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]);
                return;
            }
        }

        _mover.SetMoveDirection(Vector3.zero);

        ClearTargetPoint();
    }

    public override void Disabled()
    {
        base.Disabled();

        _mover.SetMoveDirection(Vector3.zero);
    }

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => _pathToTarget.corners.Length >= MinCornersCountInPathToMove;
}
