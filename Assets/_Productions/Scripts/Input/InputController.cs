using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class InputController : NetworkBehaviour, INetworkRunnerCallbacks
{
    [Networked]
    private NetworkButtons prevData { get; set; }
    
    public NetworkButtons PrevButtons
    {
        get => prevData;
        set => prevData = value;
    }
    
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Runner.AddCallbacks(this);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        InputData currentInput = new InputData();

        if (Input.GetKey(KeyCode.W))
            currentInput.moveDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            currentInput.moveDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            currentInput.moveDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            currentInput.moveDirection += Vector3.right;
        
        currentInput.Buttons.Set(InputButton.INTERACT, Input.GetKey(KeyCode.E));

        input.Set(currentInput);
    }
    
    #region Unused Callbacks

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

    #endregion
}
