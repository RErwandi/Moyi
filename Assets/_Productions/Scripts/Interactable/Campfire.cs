using Fusion;
using UnityEngine;

public class Campfire : Interactable
{
    public GameObject fire;
    
    [Networked(OnChanged = nameof(OnIsOnChanged))]
    public NetworkBool IsOn { get; set; }

    public override void Interact(Player player)
    {
        base.Interact(player);
        IsOn = !IsOn;
    }

    private static void OnIsOnChanged(Changed<Campfire> changed)
    {
        var newValue = changed.Behaviour.IsOn;
        changed.Behaviour.fire.SetActive(newValue);
    }
}
