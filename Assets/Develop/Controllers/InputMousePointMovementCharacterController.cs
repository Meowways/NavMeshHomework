using UnityEngine;

public class InputMousePointMovementCharacterController : Controller
{
    private const int LeftMouseButton = 0;

    private PointMovementCharacterController _pointMovementControllers;

    public InputMousePointMovementCharacterController(PointMovementCharacterController controllers)
    {
        _pointMovementControllers = controllers;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            _pointMovementControllers.SetDestinationPoint(cameraRay.origin, cameraRay.direction);
            _pointMovementControllers.CreateInstanceTargetPoint();
        }

        _pointMovementControllers.Enabled();
        _pointMovementControllers.Update(deltaTime);
    }
}
