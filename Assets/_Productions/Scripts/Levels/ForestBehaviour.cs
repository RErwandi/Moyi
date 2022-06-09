using System.Collections;
using System.Collections.Generic;
using CarterGames.Assets.AudioManager;
using Fusion;
using UnityEngine;

public class ForestBehaviour : LevelBehaviour
{
    private static ForestBehaviour _instance = null;

    public static ForestBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ForestBehaviour>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<ForestBehaviour>();
                    go.name = typeof(ForestBehaviour).ToString();
                }
            }

            return _instance;
        }
    }
    
    public Color sunColor;
    public Light sunLight;
    public GameObject nightSky;
    public GameObject morningSky;
    private int playerOnTent = 0;

    public override void Spawned()
    {
        base.Spawned();
        PlayAmbientMusic();
    }

    private void PlayAmbientMusic()
    {
        if (Object.HasStateAuthority)
        {
            NetworkMusicManager.Instance.Play("Forest BGM", 1);
        }
    }

    public void PlayerOnSleepTent(PlayerRef playerRef, Player player)
    {
        if (playerOnTent >= 2)
            return;
        
        playerOnTent++;

        if (playerOnTent >= 2)
        {
            Invoke("NextLevel", 5f);
        }
    }

    public void PlayerWakeFromTent(PlayerRef playerRef, Player player)
    {
        playerOnTent--;
        CancelInvoke("NextLevel");
    }

    public void TurnOnNightSky()
    {
        RPC_TurnOnNightSky();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_TurnOnNightSky()
    {
        sunLight.color = sunColor;
        nightSky.SetActive(true);
        morningSky.SetActive(false);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_FinishLevel()
    {
        if (FusionHelper.LocalRunner.IsClient) return;
        LoadingManager.Instance.LoadNextLevel(FusionHelper.LocalRunner);
    }
    
    private void NextLevel()
    {
        RPC_FinishLevel();
    }
}
