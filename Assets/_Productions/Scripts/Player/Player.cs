using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype controller;
    private InputController inputController;

    [Networked]
    public NetworkBool InputsAllowed { get; set; }
    
    private void Awake()
    {
        controller = GetBehaviour<NetworkCharacterControllerPrototype>();
        inputController = GetBehaviour<InputController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData input))
        {
            // Movement
            var moveDir = input.moveDirection.normalized;
            controller.Move(5f * moveDir * Runner.DeltaTime);
            
            // Jump
            if (input.GetButtonPressed(inputController.PrevButtons).IsSet(InputButton.JUMP))
            {
                controller.Jump();
            }
        }
    }
    
    public void SetInputsAllowed(bool value)
    {
        InputsAllowed = value;
    }
}
