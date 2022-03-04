namespace Game.Control
{
    public interface IRaycastable
    {
        void InteractUIDisplay();
        bool HandleRaycast(PlayerInputControl callingController);
    }
}