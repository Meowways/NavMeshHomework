using UnityEngine;

public interface IDirectionalMover 
{
    Vector3 Position { get; }

    Vector3 CurrentVelocity { get; }

    void SetMoveDirection(Vector3 direction);
}
