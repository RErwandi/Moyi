using Fusion;
using UnityEngine;

public struct InputData : INetworkInput
{
    public NetworkButtons Buttons;
    public Vector3 moveDirection;
    
    public bool GetButton(InputButton button)
    {
        return Buttons.IsSet(button);
    }

    public NetworkButtons GetButtonPressed(NetworkButtons prev)
    {
        return Buttons.GetPressed(prev);
    }

    public NetworkButtons GetButtonReleased(NetworkButtons prev)
    {
        return Buttons.GetReleased(prev);
    }
}
