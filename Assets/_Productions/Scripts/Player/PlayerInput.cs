using Fusion;
using UnityEngine.Events;

public class PlayerInput : NetworkBehaviour
{
    public InputController inputController;

    public UnityEvent<NetworkButtons> onJump;
    public UnityEvent<InputData> onMove;
    public UnityEvent<NetworkButtons> onInteract;
    public UnityEvent<NetworkButtons> onCancel;
    
    [Networked]
    public NetworkBool InputsAllowed { get; set; }

    private void Awake()
    {
        inputController = GetBehaviour<InputController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData input))
        {
            var pressed = input.GetButtonPressed(inputController.PrevButtons);
            inputController.PrevButtons = input.Buttons;

            onCancel?.Invoke(pressed);
            
            if (InputsAllowed)
            {
                onMove?.Invoke(input);
                onJump?.Invoke(pressed);
                onInteract?.Invoke(pressed);
            }
        }
    }
    
    public void SetInputsAllowed(bool value)
    {
        InputsAllowed = value;
    }
}
