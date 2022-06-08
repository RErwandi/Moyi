using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public NetworkPrefabRef playerPrefab;
    public FusionEvent onPlayerLeft;

    public static Vector3 playerSpawnPos;

    private void Awake()
    {
        playerSpawnPos = GameObject.FindGameObjectWithTag("Spawn").transform.position;
    }

    private void OnEnable()
    {
        onPlayerLeft.RegisterResponse(OnPlayerLeft);
    }

    private void OnDisable()
    {
        onPlayerLeft.RemoveResponse(OnPlayerLeft);
    }

    private void OnPlayerLeft(PlayerRef player, NetworkRunner runner)
    {
        RemovePlayer(runner, player);
    }

    public void RespawnPlayers(NetworkRunner runner)
    {
        if (!runner.IsClient)
        {
            //playerSpawnPos = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            foreach (var player in runner.ActivePlayers)
            {
                SpawnPlayer(runner, player, GameManager.Instance.GetPlayerData(player, runner).Nick);
            }
        }
    }
    
    private void SpawnPlayer(NetworkRunner runner, PlayerRef player, string nick = "")
    {
        if (runner.IsServer)
        {
            NetworkObject playerObj = runner.Spawn(playerPrefab, playerSpawnPos / 2f, Quaternion.identity, player, InitializeObjBeforeSpawn);

            PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
            data.Instance = playerObj;
            playerObj.name = data.Nick;

            //playerObj.GetComponent<Player>().Nickname = data.Nick;
        }
    }
    
    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        //var behaviour = obj.GetComponent<PlayerBehaviour>();
        //behaviour.PlayerColor = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
    }
    
    public void RemovePlayer(NetworkRunner runner, PlayerRef player)
    {
        PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
        runner.Despawn(data.Instance);
    }
}
