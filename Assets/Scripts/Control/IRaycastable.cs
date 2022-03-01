namespace Game.Control
{
    public interface IRaycastable
    {
        bool HandleRaycast(PlayerInputControl callingController);
    }
}