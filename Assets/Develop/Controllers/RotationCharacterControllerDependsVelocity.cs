public class RotationCharacterControllerDependsVelocity : Controller
{
    private AgentCharacter _character;
    private IDirectionalRotator _rotator;

    public RotationCharacterControllerDependsVelocity(AgentCharacter character, IDirectionalRotator rotator)
    {
        _character = character;
        _rotator = rotator;
    }

    protected override void UpgradeLogic(float deltaTime)
    {
        _rotator.SetRotateDirection(_character.CurrentVelocity);
    }
}
