using UnityEngine;

public class CompositeCharacterController : Controller
{
    private Controller[] _controllers;

    public CompositeCharacterController(params Controller[] controllers)
    {
        _controllers = controllers;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        foreach (Controller controller in _controllers)
            controller.Update(deltaTime);
    }

    public override void Enabled()
    {
        base.Enabled();

        foreach (Controller controller in _controllers)
            controller.Enabled();
    }

    public override void Disabled()
    {
        base.Disabled();

        foreach (Controller controller in _controllers)
            controller.Disabled();
    }
}
