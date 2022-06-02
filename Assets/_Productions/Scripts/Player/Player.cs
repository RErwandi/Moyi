using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype controller;
    private InputController inputController;

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
}
