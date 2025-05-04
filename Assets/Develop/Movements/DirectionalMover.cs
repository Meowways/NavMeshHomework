using UnityEngine;

public class DirectionalMover 
{
    private CharacterController _characterController;

    private Vector3 _currentDirection;

    private float _currentSpeed;

    public Vector3 CurrentVelocity => _characterController.velocity;

    public DirectionalMover(CharacterController characterController, float moveSpeed)
    {
        _characterController = characterController;
        _currentSpeed = moveSpeed;
    }

    public void Update(float deltatime)
    {
        _characterController.Move(_currentDirection.normalized * _currentSpeed * deltatime);
    }

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

    public void SetCurrentSpeed(float newSpeed) => _currentSpeed = newSpeed;
}
