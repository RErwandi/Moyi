using System;
using Fusion;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public bool singleUse;

    private Outline outline;
    
    /// <summary>
    /// Prevent other player to interact if TRUE
    /// </summary>
    [Networked(OnChanged = nameof(OnUseChanged))]
    public bool Used { get; set; }

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        Hide();
    }

    public virtual void Interact(Player player)
    {
        if (singleUse)
            Used = true;
    }

    public virtual void UnInteract(Player player)
    {
        if(singleUse)
            Used = false;
    }

    private static void OnUseChanged(Changed<Interactable> changed)
    {
        /*var isUsed = changed.Behaviour.Used;
        if (isUsed)
        {
            changed.Behaviour.Hide();
        }*/
    }

    public void Show()
    {
        outline.enabled = true;
    }

    public void Hide()
    {
        outline.enabled = false;
    }
}
