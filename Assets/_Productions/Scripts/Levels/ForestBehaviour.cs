using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ForestBehaviour : LevelBehaviour
{
    private int playerOnTent = 0;
    
    public void PlayerOnSleepTent(PlayerRef playerRef, Player player)
    {
        if (playerOnTent >= 2)
            return;
        
        playerOnTent++;
        Debug.Log($"Player in Tent{playerOnTent}");

        if (playerOnTent >= 2)
        {
            RPC_FinishLevel();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_FinishLevel()
    {
        if (FusionHelper.LocalRunner.IsClient) return;
        LoadingManager.Instance.LoadNextLevel(FusionHelper.LocalRunner);
    }
    
    private void NextLevel()
    {
        if (FusionHelper.LocalRunner.IsClient) return;
        LoadingManager.Instance.LoadNextLevel(FusionHelper.LocalRunner);
    }
}
