public interface IPlayerLightLevelController
{
    public void Startup();
    public void Shutdown();

    public Helper.ColorStates GetColorState();
    public void ResetColorState();
} 