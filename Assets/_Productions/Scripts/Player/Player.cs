using System;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;
    private NetworkCharacterControllerPrototype controller;
    private CameraManager cameraManager;
    public PlayerInput playerInput;
    
    private int speedFloat = Animator.StringToHash("Speed");
    private Vector3 lastPos;
    private Quaternion lastRot;

    public NetworkButtons pressed;
    
    private void Awake()
    {
        controller = GetBehaviour<NetworkCharacterControllerPrototype>();
        playerInput = GetBehaviour<PlayerInput>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void OnEnable()
    {
        playerInput.onJump.AddListener(Jump);
        playerInput.onMove.AddListener(Move);
    }

    private void OnDisable()
    {
        playerInput.onJump.RemoveListener(Jump);
        playerInput.onMove.RemoveListener(Move);
    }

    public override void Spawned()
    {
        playerInput.SetInputsAllowed(true);
        
        if (Object.HasInputAuthority)
        {
            cameraManager.SetTarget(cameraTransform);
        }
    }

    private void Move(InputData input)
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
    }
    
    private void Jump(NetworkButtons pressed)
    {
        if (pressed.IsSet(InputButton.JUMP))
        {
            Debug.Log("Jump");
            controller.Jump(); 
        }
    }

    public void MovePosition(Vector3 pos, Quaternion rot)
    {
        controller.Controller.enabled = false;
        
        lastPos = transform.position;
        lastRot = transform.rotation;
        transform.position = pos;
        transform.rotation = rot;
    }

    public void MoveLastPosition()
    {
        transform.position = lastPos;
        transform.rotation = lastRot;
        
        controller.Controller.enabled = true;
    }

    public void AnimatorTrigger(string param)
    {
        animator.SetTrigger(param);
    }
}
