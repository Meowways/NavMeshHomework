using UnityEngine;

public class DirectionalRotator 
{
    private Transform _transform;

    private Vector3 _currentDirection;

    private float _rotateSpeed;

    public DirectionalRotator(Transform transform, float moveSpeed)
    {
        _transform = transform;
        _rotateSpeed = moveSpeed;
    }

    public void Update(float deltatime)
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _rotateSpeed * deltatime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;
}
