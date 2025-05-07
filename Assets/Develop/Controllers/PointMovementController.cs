using UnityEngine;

public abstract class PointMovementController : Controller
{
    private const float MinDistanceToTarget = 0.1f;

    private GameObject _targetPointPrefab;
    private GameObject _instanceTargetPoint;

    private LayerMask _layerMask;

    protected Vector3 _targetPoint;


    public PointMovementController(GameObject targetPointPrefab, LayerMask layeMask)
    {
        _targetPointPrefab = targetPointPrefab;

        _layerMask = layeMask;
    }

    public void SetDestinationPoint(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            _targetPoint = hitInfo.point;
    }

    public void CreateInstanceDestinationPoint()
    {
        if (_instanceTargetPoint != null)
            GameObject.Destroy(_instanceTargetPoint);

        _instanceTargetPoint = GameObject.Instantiate(_targetPointPrefab, _targetPoint, Quaternion.identity);
    }

    protected void ClearTargetPoint()
    {
        if (_instanceTargetPoint != null)
        {
            GameObject.Destroy(_instanceTargetPoint);
            _instanceTargetPoint = null;
        }
    }

    protected bool IsTargetReache(float distanceToTarget) => distanceToTarget <= MinDistanceToTarget;

    protected bool IsTargetPointSet() => _instanceTargetPoint != null;

    protected abstract override void UpgradeLogic(float deltaTime);
}
