using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class LevelBehaviour : NetworkBehaviour
{
    public override void Spawned()
    {
        FindObjectOfType<PlayerSpawner>().RespawnPlayers(Runner);
        StartLevel();
    }
    
    public void StartLevel()
    {
        LoadingManager.Instance.FinishLoadingScreen();
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}
