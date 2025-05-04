using UnityEngine;

public interface IDirectionalRotator 
{
    Vector3 Position { get; }

    void SetRotateDirection(Vector3 direction);
}
