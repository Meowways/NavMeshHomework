public abstract class Controller 
{
    private bool _isEnabled;

    public virtual void Enabled() => _isEnabled = true;

    public virtual void Disabled() => _isEnabled = false;

    public void Update(float deltaTime)
    {
        if (_isEnabled == false)
            return;

        UpgradeLogic(deltaTime);
    }

    protected abstract void UpgradeLogic(float deltaTime);
}
