using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : Interactable
{
    public Transform sleepTransform;
    
    public override void Interact(Player player)
    {
        base.Interact(player);
        player.playerInput.SetInputsAllowed(false);
        player.AnimatorTrigger("Sleep");
        player.MovePosition(sleepTransform.position, sleepTransform.rotation);

        FindObjectOfType<ForestBehaviour>().PlayerOnSleepTent(player.Object.InputAuthority, player);
    }

    public override void UnInteract(Player player)
    {
        base.UnInteract(player);
        player.playerInput.SetInputsAllowed(true);
        player.AnimatorTrigger("Standby");
        player.MoveLastPosition();
    }
}
