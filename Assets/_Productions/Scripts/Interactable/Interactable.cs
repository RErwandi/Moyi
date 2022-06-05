using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        Hide();
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
