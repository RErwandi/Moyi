using UnityEngine;

public class Chair : Interactable
{
    public Transform sitTransform;
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        player.playerInput.SetInputsAllowed(false);
        player.AnimatorTrigger("Sit");
        player.MovePosition(sitTransform.position, sitTransform.rotation);
    }

    public override void UnInteract(Player player)
    {
        base.UnInteract(player);
        player.playerInput.SetInputsAllowed(true);
        player.AnimatorTrigger("Standby");
        player.MoveLastPosition();
    }
}
