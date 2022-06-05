using System;
using Fusion;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    private Interactable currentInteractable;
    private Player player;
    private Interactable interacted;

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
        if (pressed.IsSet(InputButton.INTERACT) && currentInteractable != null && interacted == null && !currentInteractable.Used)
        {
            currentInteractable.Interact(player);
            currentInteractable.Hide();
            interacted = currentInteractable;
        }
    }

    private void CancelInteract(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.CANCEL) && interacted != null)
        {
            interacted.UnInteract(player);
            interacted = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable" && currentInteractable == null)
        {
            var interactable = other.GetComponent<Interactable>();
            currentInteractable = interactable;
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
                interactable.Hide();
                currentInteractable = null;
            }
        }
    }
}
