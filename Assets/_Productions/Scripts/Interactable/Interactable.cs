using System;
using Fusion;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public GameObject canvas;
    
    [Networked]
    public bool Used { get; set; }

    private void Start()
    {
        Hide();
    }

    public virtual void Interact(Player player)
    {
        Used = true;
    }

    public virtual void UnInteract(Player player)
    {
        Used = false;
    }

    public void Show()
    {
        if(!Used)
            canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }
}
