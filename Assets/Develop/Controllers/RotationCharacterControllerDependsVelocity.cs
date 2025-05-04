public class RotationCharacterControllerDependsVelocity : Controller
{
    private IDirectionalMover _mover;
    private IDirectionalRotator _rotator;

    public RotationCharacterControllerDependsVelocity(IDirectionalMover mover, IDirectionalRotator rotator)
    {
        _mover = mover;
        _rotator = rotator;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        _rotator.SetRotateDirection(_mover.CurrentVelocity);
    }
}
