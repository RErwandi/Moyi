public class Chair : Interactable
{
    public override void Interact(Player player)
    {
        base.Interact(player);
        player.playerInput.SetInputsAllowed(false);
    }

    public override void UnInteract(Player player)
    {
        base.UnInteract(player);
        player.playerInput.SetInputsAllowed(true);
    }
}
