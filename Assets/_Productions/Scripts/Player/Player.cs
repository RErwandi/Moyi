using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private NetworkCharacterControllerPrototype controller;
    private InputController inputController;

    [Networked]
    public NetworkBool InputsAllowed { get; set; }
    
    private void Awake()
    {
        controller = GetBehaviour<NetworkCharacterControllerPrototype>();
        inputController = GetBehaviour<InputController>();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            CameraManager cameraManager = FindObjectOfType<CameraManager>();
            cameraManager.SetTarget(cameraTransform);
        }
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
