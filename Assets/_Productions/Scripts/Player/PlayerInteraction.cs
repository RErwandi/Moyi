using System;
using Fusion;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    private Interactable currentInteractable;
    private Player player;
    private bool interacting;

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
        if (pressed.IsSet(InputButton.INTERACT) && currentInteractable != null && !interacting)
        {
            player.playerInput.SetInputsAllowed(false);
            interacting = true;
        }
    }

    private void CancelInteract(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.CANCEL) && interacting)
        {
            player.playerInput.SetInputsAllowed(true);
            interacting = false;
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
