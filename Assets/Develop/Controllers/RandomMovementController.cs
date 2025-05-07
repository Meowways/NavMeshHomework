using UnityEngine;
using UnityEngine.AI;

public class RandomMovementController : Controller
{
    private const float MinDistanceToTarget = 0.05f;

    private AgentCharacter _character;

    private Vector3 _targetPoint;

    private bool _isRandomPointSet = false;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public RandomMovementController(AgentCharacter character)
    {
        _character = character;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (_isRandomPointSet == false)
            SetRandomPointAroundCharacter();

        if (_character.TryGetPath(_targetPoint, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReache(distanceToTarget) == false)
            {
                _character.ResumeMove();
                _character.SetDestination(_targetPoint);
                return;
            }
        }
        else
            SetRandomPointAroundCharacter();

        _character.StopMove();
        _isRandomPointSet = false;
    }

    private void SetRandomPointAroundCharacter()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));

        _targetPoint = _character.transform.position + randomDirection.normalized * 5;

        _isRandomPointSet = true;
    }

    private bool IsTargetReache(float distanceToTarget) => distanceToTarget <= MinDistanceToTarget;
}
