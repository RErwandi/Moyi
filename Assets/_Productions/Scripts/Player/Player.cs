using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;
    private NetworkCharacterControllerPrototype controller;
    private InputController inputController;
    private CameraManager cameraManager;
    
    private int speedFloat = Animator.StringToHash("Speed");

    [Networked]
    public NetworkBool InputsAllowed { get; set; }
    
    private void Awake()
    {
        controller = GetBehaviour<NetworkCharacterControllerPrototype>();
        inputController = GetBehaviour<InputController>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            cameraManager.SetTarget(cameraTransform);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData input))
        {
            // Movement
            // Get camera forward direction, without vertical component.
            Vector3 forward = input.aimForwardVector;

            // Player is moving on ground, Y component of camera facing is not relevant.
            forward.y = 0.0f;
            forward = forward.normalized;

            // Calculate target direction based on camera forward and direction key.
            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            var moveDir = input.moveDirection.normalized;
            
            Vector3 targetDirection;
            targetDirection = forward * moveDir.z + right * moveDir.x;
            controller.Move(5f * targetDirection * Runner.DeltaTime);
            
            var speed = Vector3.ClampMagnitude(moveDir, 1f).magnitude;
            animator.SetFloat(speedFloat, speed, 0.1f, Runner.DeltaTime);
            
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
