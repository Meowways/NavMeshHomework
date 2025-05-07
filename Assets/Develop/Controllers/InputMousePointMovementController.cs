using UnityEngine;

public class InputMousePointMovementController : Controller
{
    private const int LeftMouseButton = 0;

    private PointMovementController _pointMovementControllers;
    private AgentCharacter _character;

    public InputMousePointMovementController(PointMovementController controllers, AgentCharacter character)
    {
        _pointMovementControllers = controllers;
        _character = character;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _pointMovementControllers.SetDestinationPoint(cameraRay.origin, cameraRay.direction);
            _pointMovementControllers.CreateInstanceDestinationPoint();

            _character.SetNotBored();
        }

        _pointMovementControllers.Enabled();
        _pointMovementControllers.Update(deltaTime);
    }
}
