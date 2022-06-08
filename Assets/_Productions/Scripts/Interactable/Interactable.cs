using System;
using Fusion;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public GameObject canvas;
    public bool singleUse;
    
    /// <summary>
    /// Prevent other player to interact if TRUE
    /// </summary>
    [Networked(OnChanged = nameof(OnUseChanged))]
    public bool Used { get; set; }

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
        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }
}
