using System;
using Fusion;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    private Interactable currentInteractable;
    private Player player;

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
            currentInteractable.Interact(player);
            currentInteractable.Hide();
            Debug.Log($"Interacted with {currentInteractable.gameObject.name}");
        }
    }

    private void CancelInteract(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.CANCEL) && currentInteractable != null)
        {
            currentInteractable.UnInteract(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable" && currentInteractable == null && Object.HasInputAuthority)
        {
            var interactable = other.GetComponent<Interactable>();
            currentInteractable = interactable;
            interactable.Show();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable" && Object.HasInputAuthority)
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
