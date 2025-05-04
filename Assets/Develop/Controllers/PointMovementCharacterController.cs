using UnityEngine;
using UnityEngine.AI;

public class PointMovementCharacterController : Controller
{
    private const int MinCornersCountInPathToMove = 2;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private const float MinDistanceToTarget = 0.1f;

    private GameObject _targetPointPrefab;
    private GameObject _instanceTargetPoint;

    private IDirectionalMover _mover;

    private Vector3 _targetPoint;

    private NavMeshPath _pathToTarget = new NavMeshPath();
    private NavMeshQueryFilter _queryFilter;

    public PointMovementCharacterController(IDirectionalMover mover, NavMeshQueryFilter queryFilter, GameObject targetPointPrefab)
    {
        _mover = mover;

        _queryFilter = queryFilter;

        _targetPointPrefab = targetPointPrefab;
    }

    public void SetDestinationPoint(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo))
            _targetPoint = hitInfo.point;
    }

    public void CreateInstanceTargetPoint()
    {
        if (_instanceTargetPoint != null)
            GameObject.Destroy(_instanceTargetPoint);

        _instanceTargetPoint = GameObject.Instantiate(_targetPointPrefab, _targetPoint, Quaternion.identity);
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (_instanceTargetPoint != null && NavMeshUtils.TryGetPath(_mover.Position, _targetPoint, _queryFilter, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReache(distanceToTarget) == false && EnoughCornersInPath(_pathToTarget))
            {
                _mover.SetMoveDirection(_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]);
                return;
            }
        }

        _mover.SetMoveDirection(Vector3.zero);

        GameObject.Destroy(_instanceTargetPoint);
        _instanceTargetPoint = null;
    }

    public override void Disabled()
    {
        base.Disabled();

        _mover.SetMoveDirection(Vector3.zero);
    }

    private bool IsTargetReache(float distanceToTarget) => distanceToTarget <= MinDistanceToTarget;

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => _pathToTarget.corners.Length >= MinCornersCountInPathToMove;

}
