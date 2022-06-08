using System;
using Fusion;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    [Networked]
    private Interactable currentInteractable { get; set; }
    private Player player;

    [Networked(OnChanged = nameof(OnInteractingChanged))]
    private NetworkBool Interacting { get; set; }
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.playerInput.onInteract.AddListener(Interact);
        player.playerInput.onCancel.AddListener(CancelInteract);
    }

    private void OnDisable()
    {
        player.playerInput.onInteract.RemoveListener(Interact);
        player.playerInput.onCancel.RemoveListener(CancelInteract);
    }

    private void Interact(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.INTERACT) && currentInteractable != null && !currentInteractable.Used)
        {
            Interacting = true;
        }
    }

    private void CancelInteract(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.CANCEL) && currentInteractable != null)
        {
            Interacting = false;
        }
    }

    private static void OnInteractingChanged(Changed<PlayerInteraction> changed)
    {
        var isInteracting = changed.Behaviour.Interacting;
        if (isInteracting)
        {
            changed.Behaviour.currentInteractable.Interact(changed.Behaviour.player);

            if (!changed.Behaviour.currentInteractable.singleUse)
            {
                changed.Behaviour.Interacting = false;
                return;
            }

            if(changed.Behaviour.Object.HasInputAuthority)
                changed.Behaviour.currentInteractable.Hide();
        }
        else
        {
            changed.Behaviour.currentInteractable.UnInteract(changed.Behaviour.player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable" && currentInteractable == null)
        {
            var interactable = other.GetComponent<Interactable>();
            currentInteractable = interactable;
            
            if(Object.HasInputAuthority)
                interactable.Show();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            var interactable = other.GetComponent<Interactable>();
            if (interactable == currentInteractable)
            {
                if(Object.HasInputAuthority)
                    interactable.Hide();
                
                currentInteractable = null;
            }
        }
    }
}
